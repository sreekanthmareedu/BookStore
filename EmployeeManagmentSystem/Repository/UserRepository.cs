using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Model;
using BookStoreAPI.Model.DTO.UserDTO;
using BookStoreAPI.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreAPI.Repository
{
    public class UserRepository  : IUserRepository
    {
        private readonly ApplicationDBContext _db;

        private string _secretKey;

        private readonly IMapper _mapper;



        public UserRepository(ApplicationDBContext db, IMapper mapper, IConfiguration configuration) { 
        
        _db = db;
        _mapper  = mapper;
            _secretKey = configuration.GetValue<String>("ApiSettings:secret");
        
        }

        public bool IsUniqueUser(string username)
        {
           var user = _db.Users.FirstOrDefault(u=>u.Name.ToLower() == username.ToLower());
            if(user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<UserResponseDTO> login(UserRequestDTO dto)
        {
            var user =  _db.Users.FirstOrDefault(u => u.Email.ToLower() == dto.Email.ToLower()
            &&u.Password==dto.Password);
            if(user == null)
            {
                return new UserResponseDTO()
                {
                    Token = "",
                    UserInfo = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)

                }),

                Expires = DateTime.UtcNow.AddDays(4),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            UserResponseDTO userResponseDTO = new UserResponseDTO()
            {

                Token = tokenHandler.WriteToken(token),
                UserInfo = user
            };


            return userResponseDTO;




        }

        public async Task<User> Register(RegistrationDTO dto)
        {
            User user = _mapper.Map<User>(dto);
            await _db.Users.AddAsync(user);
           await _db.SaveChangesAsync();
            user.Password = "";
            return user;

            
        }
    }
}

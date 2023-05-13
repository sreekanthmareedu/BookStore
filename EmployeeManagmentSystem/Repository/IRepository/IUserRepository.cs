using BookStoreAPI.Model;
using BookStoreAPI.Model.DTO.UserDTO;

namespace BookStoreAPI.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {

        bool IsUniqueUser(string username);

        Task<UserResponseDTO> login (UserRequestDTO dto);

        Task<User> Register(RegistrationDTO dto);

         Task<User> UpdateAsync(User entity);

    }
}

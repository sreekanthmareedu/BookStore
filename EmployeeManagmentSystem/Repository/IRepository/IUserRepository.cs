using BookStoreAPI.Model;
using BookStoreAPI.Model.DTO.UserDTO;

namespace BookStoreAPI.Repository.IRepository
{
    public interface IUserRepository
    {

        bool IsUniqueUser(string username);

        Task<UserResponseDTO> login (UserRequestDTO dto);

        Task<User> Register(RegistrationDTO dto);


    }
}

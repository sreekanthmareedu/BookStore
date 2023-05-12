using BookStoreAPI.Model;
using BookStoreAPI.Model.DTO.UserDTO;
using BookStoreAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStoreAPI.Controllers
{
    [Controller]
    [Route("/api/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _user;

        public APIResponses responses;
        public UserController(IUserRepository user) {

            _user = user;

            responses = new APIResponses();

        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserRequestDTO dto)
        {
            var response = await _user.login(dto);
            if (response.UserInfo == null || string.IsNullOrEmpty(response.Token)) {

                responses.IsSuccess = false;
                responses.StatusCode = HttpStatusCode.BadRequest;

                responses.ErrorMessage.Add("Invalid username/password");
                return BadRequest(responses);

            }
            responses.StatusCode = HttpStatusCode.OK;
            responses.Result = response;
            return Ok(responses);


        }
        [HttpPost("Create")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO dto)
        {

            bool uniqueName = _user.IsUniqueUser(dto.Name);
            if (!uniqueName)
            {
                responses.IsSuccess = false;
                responses.StatusCode = HttpStatusCode.BadRequest;
                responses.ErrorMessage.Add("user name already exist");
                return BadRequest(responses);
            }
            var userinfo = await _user.Register(dto);
            if (userinfo == null)
            {
                responses.IsSuccess = false;
                responses.StatusCode = HttpStatusCode.BadRequest;
                responses.ErrorMessage.Add("Error while registering user");
                return BadRequest(responses);
            }



            return Ok(responses);


        }




        }
    }

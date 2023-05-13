using AutoMapper;
using BookStoreAPI.Model;
using BookStoreAPI.Model.DTO;
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

        private readonly IMapper _mapper;



        protected APIResponses responses;
        public UserController(IUserRepository user, IMapper mapper)
        {

            _user = user;

            _mapper = mapper;

            responses = new APIResponses();

        }
        //====================================================================================================
        //============================== Authentication ========================================================

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromBody] UserRequestDTO dto)
        {
            var response = await _user.login(dto);
            if (response.UserInfo == null || string.IsNullOrEmpty(response.Token))
            {

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateUser([FromBody] RegistrationDTO dto)
        {
            if (!ModelState.IsValid)
            {




                return BadRequest(ModelState);

            }

            bool uniqueName = _user.IsUniqueUser(dto.Email);
            if (!uniqueName)
            {
                responses.IsSuccess = false;
                responses.StatusCode = HttpStatusCode.BadRequest;
                responses.ErrorMessage.Add("Email ID already exist");
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


            responses.StatusCode = HttpStatusCode.Created;
            return Ok(responses);


        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponses>> UpdateUser(int id, [FromBody] UserUpdateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {




                    return BadRequest(ModelState);

                }
                if (id == null || (dto.Id != id))
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);

                }
                if (await _user.GetAsync(u => u.Email.ToLower() == dto.Email.ToLower()) != null )
                {
                    responses.Result = "Mail Id Already Registered ";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    responses.IsSuccess = false;
                    return BadRequest(responses);
                }
                User users = _mapper.Map<User>(dto);

                await _user.UpdateAsync(users);

                responses.StatusCode = HttpStatusCode.NoContent;
                responses.Result = "User with Id : " + id + "  Updated successfully";
                return Ok(responses);


            }
            catch (Exception ex)
            {
                responses.IsSuccess = false;
                responses.ErrorMessage = new List<string> { ex.Message };
            }
            return responses;
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        
        public async Task<ActionResult<APIResponses>> GetUser(int id)
        {
            try
            {

                if (id <= 0)
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);

                }
                var users = await _user.GetAsync(u => u.Id == id);

                if (users == null)
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.OK;
                    return Ok(responses);

                }
                responses.Result = _mapper.Map<UserDTO>(users);
                responses.StatusCode = HttpStatusCode.OK;
                return Ok(responses);

            }
            catch (Exception ex)
            {
                responses.IsSuccess = false;
                responses.ErrorMessage = new List<String> { ex.Message };
            }

            return responses;

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponses>> RemoveUser(int id)
        {
            try
            {
                if (id == null)
                {
                    responses.Result = "Invalid Id";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);
                }
                User users = await _user.GetAsync(u => u.Id == id);
                if (users == null)
                {
                    responses.Result = "Invalid User Id";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);


                }
                await _user.RemoveAsync(users);
                responses.IsSuccess = true;
                responses.StatusCode = HttpStatusCode.NoContent;
                responses.Result = "User with Id :" + id + "  deleted successfully";
                return Ok(responses);
            }
            catch (Exception ex)
            {
                responses.IsSuccess = false;
                responses.ErrorMessage = new List<string> { ex.Message };
            }
            return responses;


        }

        //=======================================================================================================
        //==============================Get All Users ===========================================================

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponses>> GetAllAuthor()
        {
            try
            {


                IEnumerable<UserDTO> pub = _mapper.Map<List<UserDTO>>(await _user.GetAllAsync());
                responses.StatusCode = HttpStatusCode.OK;
                responses.Result = pub;
                return Ok(responses);
            }
            catch (Exception ex)
            {
                responses.IsSuccess = false;
                responses.ErrorMessage =
                    new List<string> { ex.Message };

            }
            return responses;



        }
    }
}

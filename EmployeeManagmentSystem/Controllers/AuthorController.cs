using AutoMapper;
using BookStoreAPI.Model;
using BookStoreAPI.Model.DTO;
using BookStoreAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStoreAPI.Controllers
{
    [Controller]
    [Route("/api/Author")]
    public class AuthorController : ControllerBase
    {

        private readonly IAuthorRepository _dbAuthor;
        private readonly IMapper _mapper;
        protected APIResponses responses;

        public AuthorController(IMapper mapper, IAuthorRepository dbAuthor)
        {
            _mapper = mapper;
            _dbAuthor = dbAuthor;
            responses = new APIResponses();
        }


        //=====================================================================================
        //====================================Get All Author Info =========================

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
       
        public async Task<ActionResult<APIResponses>> GetAllAuthor()
        {
            try
            {
                // Publisher publisher = await _dbPublisher.GetAllAsync();

                IEnumerable<AuthorDTO> pub = _mapper.Map<List<AuthorDTO>>(await _dbAuthor.GetAllAsync());
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

        //===================================================================================
        //======================== Get single Author ======================================

        [HttpGet("{id:int}", Name = "Author")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
       

        public async Task<ActionResult<APIResponses>> GetPublisher(int id)
        {
            try
            {

                if (id <= 0)
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);

                }
                var author = await _dbAuthor.GetAsync(u => u.Id == id);

                if (author == null)
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.NoContent;
                    return Ok(responses);
                   
                }
                responses.Result = _mapper.Map<AuthorDTO>(author);
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


        //=========================================================================================
        //========================== Create Author ==========================================

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponses>> CreateAuthor([FromBody] AuthorCreateDTO dto)

        {

            try
            {
                if (!ModelState.IsValid)
                {
                    
                    
                    
                    
                    return BadRequest(ModelState);

                }

                if (dto == null)
                {
                    ModelState.AddModelError("Custom Error", "Invalid Details");
                    return BadRequest(ModelState);

                }
                if (await _dbAuthor.GetAsync(u => u.Name.ToLower() == dto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "Name already Exist");
                    return BadRequest(ModelState);
                }
                Author author = _mapper.Map<Author>(dto);
                await _dbAuthor.CreateAsync(author);
                responses.Result = _mapper.Map<AuthorDTO>(author);
                responses.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("Author", new { id = author.Id }, responses);
            }
            catch (Exception ex)
            {

                responses.IsSuccess = false;

                responses.ErrorMessage = new List<string> { ex.Message };
            }
            return responses;


        }

        //=================================================================================================
        //=========================Update Author info ===================================================

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult<APIResponses>> UpdateAuthor(int id, [FromBody] AuthorUpdateDTO dto)
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
                Author author = _mapper.Map<Author>(dto);

                await _dbAuthor.UpdateAsync(author);
                
                responses.StatusCode = HttpStatusCode.NoContent;
                responses.Result = "Author with Id : " + id + "  Updated successfully";
                return Ok(responses);
                

            }
            catch (Exception ex)
            {
                responses.IsSuccess = false;
                responses.ErrorMessage = new List<string> { ex.Message };
            }
            return responses;
        }

        //===========================================================================================================================
        //==================================== Delete the Author ========================================================
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<ActionResult<APIResponses>> RemoveAuthor(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);
                }
                Author author = await _dbAuthor.GetAsync(u => u.Id == Id);
                if (author == null)
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);


                }
                await _dbAuthor.RemoveAsync(author);
                responses.IsSuccess = true;
                responses.StatusCode = HttpStatusCode.NoContent;
                responses.Result = "Author with Id :" + Id + "  deleted successfully";
                return Ok(responses);
            }
            catch (Exception ex)
            {
                responses.IsSuccess = false;
                responses.ErrorMessage = new List<string> { ex.Message };
            }
            return responses;

        }





    }
}

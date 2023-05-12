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
        public async Task<ActionResult<APIResponses>> GetAllAuthor()
        {
            try
            {
                // Publisher publisher = await _dbPublisher.GetAllAsync();

                IEnumerable<AuthorDTO> pub = _mapper.Map<List<AuthorDTO>>(await _dbAuthor.GetAllAsync());
                responses.IsSuccess = true;
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
        public async Task<ActionResult<APIResponses>> GetPublisher(int id)
        {
            try
            {

                if (id <= 0)
                {


                }
                var author = await _dbAuthor.GetAsync(u => u.Id == id);

                if (author == null)
                {
                    return NoContent();
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
        public async Task<ActionResult<APIResponses>> CreateAuthor([FromBody] AuthorCreateDTO dto)

        {

            try
            {

                if (dto == null)
                {

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

        /*[HttpPut]
        public async Task<ActionResult<APIResponses>> UpdatePublisher(int id, [FromBody] AuthorUpdateDTO dto)
        {
            try
            {
                if (id == null || (dto.Id != id))
                {
                    return BadRequest();

                }
                Publisher publisher = _mapper.Map<Publisher>(dto);


                await _dbAuthor.UpdateAsync(publisher);
                await _dbPublisher.SaveAsync();
                responses.IsSuccess = true;
                responses.StatusCode = HttpStatusCode.NoContent;
                responses.Result = "Publisher with Id : " + id + "  Updated successfully";
                return Ok(responses);
                ;

            }
            catch (Exception ex)
            {
                responses.IsSuccess = false;
                responses.ErrorMessage = new List<string> { ex.Message };
            }
            return responses;
        }*/

        //===========================================================================================================================
        //==================================== Delete the publisher ========================================================
        [HttpDelete]
        public async Task<ActionResult<APIResponses>> RemoveAuthor(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();
                }
                Author author = await _dbAuthor.GetAsync(u => u.Id == Id);
                if (author == null)
                {
                    return BadRequest();
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

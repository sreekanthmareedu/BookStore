using AutoMapper;
using BookStoreAPI.Model;
using BookStoreAPI.Model.DTO;
using BookStoreAPI.Model.DTO.BooksDTO;
using BookStoreAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStoreAPI.Controllers
{
    [Controller]
    [Route("/api/Book")]
    public class BookController : ControllerBase
    {

        private readonly IAuthorRepository _dbAuthor;
        private readonly IBookRepository _dbBook;
        private readonly IPublisherRepository _dbPublisher;
        private readonly IMapper _mapper;
        protected APIResponses responses;



        public BookController(IMapper mapper, IAuthorRepository dbAuthor ,IPublisherRepository dbPublisher, IBookRepository dbBook)
        {
            _mapper = mapper;
            _dbAuthor = dbAuthor;
            _dbBook = dbBook;
            _dbPublisher = dbPublisher;
            responses = new APIResponses();
        }


        //=====================================================================================
        //====================================Get All Books Info =========================

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponses>> GetAllBooks()
        {
            try
            {
                // Publisher publisher = await _dbPublisher.GetAllAsync();

                IEnumerable<BookDTO> pub = _mapper.Map<List<BookDTO>>(await _dbBook.GetAllAsync());
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
        //======================== Get Individual Book Info ======================================

        [HttpGet("{id:int}", Name = "Book")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        
        public async Task<ActionResult<APIResponses>> GetBook(int id)
        {
            try
            {

                if (id <= 0)
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);

                }
                var book = await _dbBook.GetAsync(u => u.Id == id);

                if (book == null)
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.OK;
                    return Ok(responses);
                   
                }
                responses.Result = _mapper.Map<BookDTO>(book);
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
        //========================== Create Book ==========================================

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
       
        public async Task<ActionResult<APIResponses>> CreateBook([FromBody]BookCreateDTO dto)

        {

            try
            {

                if (dto == null)
                {

                }
                if (await _dbBook.GetAsync(u => u.Id == dto.Id) != null)
                {
                    ModelState.AddModelError("Custom Error", "Book already Exist");
                    return BadRequest(ModelState);
                }
                var PublisherID = await _dbPublisher.GetAsync(u=>u.Id == dto.publishers);
                var AuthorID = await _dbAuthor.GetAsync(u=>u.Id==dto.authors);

        
                if(PublisherID == null)
                {
                    ModelState.AddModelError("Custom Error", "PublisherID is Invalid");
                    return BadRequest(ModelState);
                }
                if (AuthorID == null)
                {
                    ModelState.AddModelError("Custom Error", "AuthorID is Invalid");
                    return BadRequest(ModelState);
                }

                Books book =  _mapper.Map<Books>(dto);
                
                await _dbBook.CreateAsync(book);
                responses.Result = _mapper.Map<BookDTO>(book);
                responses.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("Book", new { id = dto.Id }, responses);
            }
            catch (Exception ex)
            {

                responses.IsSuccess = false;
                responses.ErrorMessage = new List<string> { ex.Message };
            }
            return responses;


        }

        //=================================================================================================
        //=========================Update Book info ===================================================

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<APIResponses>> UpdateBook(int id, [FromBody] BookUpdateDTO dto)
        {
            try
            {
                if (id == null || (dto.Id != id))
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);

                }
                if (await _dbPublisher.GetAsync(u => u.Id == dto.Publisher) == null)
                {
                    ModelState.AddModelError("Custom Error", "PublisherID is Invalid");
                    return BadRequest(ModelState);
                }
                if (await _dbAuthor.GetAsync(u => u.Id == dto.Author) == null)
                {
                    ModelState.AddModelError("Custom Error", "AuthorID is Invalid");
                    return BadRequest(ModelState);
                }
                Books book = _mapper.Map<Books>(dto);

                await _dbBook.UpdateAsync(book);
                
                responses.StatusCode = HttpStatusCode.NoContent;
                responses.Result = "Book with Id : " + id + "  Updated successfully";
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
        //==================================== Delete the Book ========================================================
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        

        public async Task<ActionResult<APIResponses>> RemoveBook(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);
                }
                Books book = await _dbBook.GetAsync(u => u.Id == Id);
                if (book == null)
                {
                    responses.Result = "Invalid ID";
                    responses.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(responses);


                }
                await _dbBook.RemoveAsync(book);
                responses.IsSuccess = true;
                responses.StatusCode = HttpStatusCode.NoContent;
                responses.Result = "Book with Id :" + Id + "  deleted successfully";
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

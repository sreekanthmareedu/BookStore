using AutoMapper;
using Azure;
using BookStoreAPI.Model;
using BookStoreAPI.Model.DTO;
using BookStoreAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;

namespace BookStoreAPI.Controllers
{
    [Controller]
    [Route("/api/Publisher")]
    public class PublisherController : ControllerBase
    {

        private readonly IPublisherRepository _dbPublisher;
        private readonly IMapper _mapper;
        protected APIResponses responses;

        public PublisherController(IMapper mapper, IPublisherRepository dbPublisher)
        {
            _mapper = mapper;
            _dbPublisher = dbPublisher;
            responses = new APIResponses();
        }

//=====================================================================================
//====================================Get All publisher Info =========================

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponses>> GetAllPublisher()
        {
            try
            {
               // Publisher publisher = await _dbPublisher.GetAllAsync();
                
                IEnumerable<PublisherDTO> pub = _mapper.Map<List<PublisherDTO>>(await _dbPublisher.GetAllAsync());
                responses.IsSuccess = true;
                responses.Result = pub;
                responses.StatusCode = HttpStatusCode.OK;


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
        //======================== Get single publisher ======================================

        [HttpGet("{id:int}", Name = "Publisher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponses>> GetPublisher(int id)
        {
            try
            {

                if (id <= 0)
                {


                }
                var publisher = await _dbPublisher.GetAsync(u => u.Id == id);

                if (publisher == null)
                {
                    return NoContent();
                }
                responses.Result = _mapper.Map<PublisherDTO>(publisher);
                responses.StatusCode = HttpStatusCode.OK;
                return Ok(responses);

            }
            catch(Exception ex)
            {
                responses.IsSuccess = false;
                responses.ErrorMessage = new List<String> { ex.Message };
            }

            return responses;

        }


        //=========================================================================================
        //========================== Create Publisher ==========================================

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponses>> CreatePublisher([FromBody]PublisherCreateDTO dto)

        {

            try
            {

                if (dto == null)
                {

                }
                if (await _dbPublisher.GetAsync(u => u.Name.ToLower() == dto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("Custome Error", "Name already Exist");
                    return BadRequest(ModelState);
                }
                Publisher publisher = _mapper.Map<Publisher>(dto);
                await _dbPublisher.CreateAsync(publisher);
                responses.Result = _mapper.Map<PublisherDTO>(publisher);
                responses.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("Publisher", new { id = publisher.Id }, responses);
            }
            catch(Exception ex)
            {

                responses.IsSuccess = false;
                 responses.ErrorMessage = new List<string> { ex.Message };
            }
            return responses;


        }

        //=================================================================================================
        //=========================Update Publisher info ===================================================

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponses>> UpdatePublisher(int id,[FromBody]PublisherUpdateDTO dto)
        {
            try
            {
                if (id == null || (dto.Id != id))
                {
                    return BadRequest();

                }
                Publisher publisher = _mapper.Map<Publisher>(dto);


                await _dbPublisher.UpdateAsync(publisher);
                await _dbPublisher.SaveAsync();
                responses.IsSuccess = true;
                responses.StatusCode = HttpStatusCode.NoContent;
                responses.Result = "Publisher with Id : " + id + "  Updated successfully";
                return Ok(responses);
              ;

            }
            catch(Exception ex)
            { 
                responses.IsSuccess = false;
                responses.ErrorMessage = new List<string> { ex.Message};
            }
            return responses;
        }

        //===========================================================================================================================
        //==================================== Delete the publisher ========================================================
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponses>> RemovePublisher(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();
                }
                Publisher publisher = await _dbPublisher.GetAsync(u => u.Id == Id);
                if (publisher == null)
                {
                    return BadRequest();
                }
                await _dbPublisher.RemoveAsync(publisher);
                responses.IsSuccess = true;
                responses.StatusCode = HttpStatusCode.NoContent;
                responses.Result = "publisher with Id :" + Id + "  deleted successfully";
                return Ok(responses);
            }
                catch(Exception  ex) 
            {
                responses.IsSuccess = false;
                responses.ErrorMessage = new List<string> { ex.Message};
            }
            return responses;

        }

}
}

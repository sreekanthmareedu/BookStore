using System.Net;

namespace BookStoreAPI.Model
{
    public class APIResponses
    {

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }

        public List<String> ErrorMessage { get; set; }

        public Object Result { get; set; }  


    }
}

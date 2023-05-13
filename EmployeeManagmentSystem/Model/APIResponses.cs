using System.Net;

namespace BookStoreAPI.Model
{
    public class APIResponses
    {
        public APIResponses() {


            ErrorMessage = new List<string>();
                }
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; } = true;

        public List<String> ErrorMessage { get; set; }

        public Object Result { get; set; }  


    }
}

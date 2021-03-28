using System.Net;

namespace RockPaperScissorsGame.Model
{
    public class ResponseError
    {
        public HttpStatusCode ErrorCode { get; set; }

        public string Description { get; set; }
    }
}

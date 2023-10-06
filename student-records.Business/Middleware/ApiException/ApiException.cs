using System.Net;

namespace student_records.Business.Middleware.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public ApiException(string message) : base(message)
        {
        }
    }
}
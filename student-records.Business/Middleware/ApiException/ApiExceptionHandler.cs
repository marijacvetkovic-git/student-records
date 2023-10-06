using System.Net;

namespace student_records.Business.Middleware.Exceptions
{
    public static class ApiExceptionHandler
    {
        public static void ThrowApiException(HttpStatusCode statusCode, string message)
        {
            ApiException apiException = new(message)
            {
                StatusCode = statusCode
            };

            throw apiException;
        }

        public static void StringNotNullOrEmpty(string field, string fieldName)
        {
            if (string.IsNullOrEmpty(field))
            {
                ThrowApiException(HttpStatusCode.BadRequest, fieldName + " is null or empty.");
            }
        }

        public static void ObjectNotNull(this object obj, string message)
        {
            if (obj == null)
            {
                ThrowApiException(HttpStatusCode.BadRequest, message + " doesn't exist.");
            }
        }
        public static void YearOfStudiesBoundaryException(int obj, string message)
        {
            if (obj<1 || obj>6)
            {
                ThrowApiException(HttpStatusCode.BadRequest, message + " is not valid");
            }
        }
        public static void InvalidDateTime(DateTime obj, string message)
        {
            if (obj >= DateTime.Now)
            {
                ThrowApiException(HttpStatusCode.BadRequest, message + " is not valid");
            }
        }
        public static void InvalidIndexNumber(int obj, string message)
        {
            if (obj <=0)
            {
                ThrowApiException(HttpStatusCode.BadRequest, message + " is not valid");
            }
        }
        public static void InvalidJMBG(string obj, string message)
        {
            if (obj.Length!=13)
            {
                ThrowApiException(HttpStatusCode.BadRequest, message + " is not valid");
            }
        }
        public static void InvalidTelephoneNumber(string obj, string message)
        {
            if (obj.Length != 10)
            {
                ThrowApiException(HttpStatusCode.BadRequest, message + " is not valid");
            }
        }
        public static void InvalidNumberOfCharacters(string obj, string message)
        {
            if (obj.Length>20 || string.IsNullOrEmpty(obj))
            {
                ThrowApiException(HttpStatusCode.BadRequest, message + " is not valid");
            }
        }
        public static void ValueNotAcceptable(this int obj, string message)
        {
            if (obj <=5 || obj >10)
            {
                ThrowApiException(HttpStatusCode.NotAcceptable, message + " is not correct.");
            }
        }
        public static void GradeAlreadyExist(bool obj, string message)
        {
            if (obj == true)
            {
                ThrowApiException(HttpStatusCode.BadRequest, message + "already passed exam");
            }
        }
        public static void TypeError(string type, string message)
        {
            if (!String.Equals("O",type) && !String.Equals("I", type))
            {
                ThrowApiException(HttpStatusCode.BadRequest, message + "does not exist.");
            }
        }
        
    }
}
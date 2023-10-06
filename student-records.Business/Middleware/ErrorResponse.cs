namespace student_records.Business.Middleware
{
    public class ErrorResponse
    {
        public ErrorResponse(string message)
        {
            ErrorMessage = message;
        }

        public string ErrorMessage { get; set; }
    }
}
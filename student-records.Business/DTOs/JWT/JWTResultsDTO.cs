namespace student_records.Business.DTOs.JWT
{
    public class JWTResultsDTO
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}

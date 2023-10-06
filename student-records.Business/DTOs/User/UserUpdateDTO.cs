namespace student_records.Business.DTOs.User
{
    public class UserUpdateDTO
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? Email { get; set; } 
        public string? Password { get; set; }
    }
}

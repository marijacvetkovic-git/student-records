namespace student_records.Business.DTOs.Subject
{
    public class SubjectUpdateDTO
    {
        public string? Name { get; set; } 
        public string? Code { get; set; } 
        public int? YearOfStudies { get; set; }
        public int? Espb { get; set; }
        public string? TypeOf { get; set; }
    }
}

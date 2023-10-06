namespace student_records.Business.DTOs.Grade
{
    public class GradeCreateDTO
    {
        public int StudentId { get; set; }
        public  int SubjectId { get; set; }
        public int Value { get; set; }
    }
}

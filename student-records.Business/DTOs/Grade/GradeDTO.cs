namespace student_records.Business.DTOs.Grade;

public class GradeDTO
{
    public int Id { get; set; }
    public int Value { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
}
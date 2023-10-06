namespace students_records.Data.Models
{
    public partial class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime DateOfGrading { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}

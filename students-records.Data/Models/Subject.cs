namespace students_records.Data.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Grades = new HashSet<Grade>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int YearOfStudies { get; set; }
        public int Espb { get; set; }
        public string TypeOf { get; set; } = null!;

        public virtual ICollection<Grade> Grades { get; set; }
    }
}

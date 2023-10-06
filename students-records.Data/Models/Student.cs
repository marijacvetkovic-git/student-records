namespace students_records.Data.Models
{
    public partial class Student
    {
        public Student()
        {
            Grades = new HashSet<Grade>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Jmbg { get; set; }
        public string ParentName { get; set; }
        public int IndexNumber { get; set; }
        public int YearOfStudies { get; set; }
        public int? CurrentEspb { get; set; }
        public double? CurrentAvarageRate { get; set; }
        public string TelephoneNumber { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}

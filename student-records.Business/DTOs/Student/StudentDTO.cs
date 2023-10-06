namespace student_records.Business.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Jmbg { get; set; }
        public string ParentName { get; set; }
        public int IndexNumber { get; set; }
        public int YearOfStudies { get; set; }
        public int CurrentEspb { get; set; }
        public double CurrentAvarageRate { get; set; }
        public string TelephoneNumber { get; set; }
    }
}

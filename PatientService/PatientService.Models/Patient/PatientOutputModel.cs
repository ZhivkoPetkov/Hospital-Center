namespace PatientService.Models.Patient
{
    public class PatientOutputModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string NAN { get; set; }
        public bool IsVaccinated {get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PatientService.Domains
{
    public class Patient : BaseEntity
    {
        public Patient()
        {
            this.IsVaccinated = false;
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }

        // National identification number
        [Required]
        public string NAN { get; set; }

        public bool IsVaccinated { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace VaccineService.Models.Patient
{
    public class PatientInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string NAN { get; set; }

        [Required]
        public string Event { get; set; }
    }
}

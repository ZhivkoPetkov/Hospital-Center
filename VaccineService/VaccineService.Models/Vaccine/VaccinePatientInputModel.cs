using System.ComponentModel.DataAnnotations;

namespace VaccineService.Models.Vaccine
{
    public class VaccinePatientInputModel
    {
        [Required]
        public int VaccineId { get; set; }
        [Required]
        public int PatientId { get; set; }
    }
}

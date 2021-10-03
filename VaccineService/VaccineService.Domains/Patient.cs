using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaccineService.Domains
{
    public class Patient : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }

        // National identification number
        [Required]
        public string NAN { get; set; }

        public virtual ICollection<PatientVaccine> Vaccnies { get; set; }
    }
}

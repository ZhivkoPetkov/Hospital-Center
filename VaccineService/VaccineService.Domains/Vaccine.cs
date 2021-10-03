using System.Collections.Generic;

namespace VaccineService.Domains
{
    public class Vaccine : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PatientVaccine> Patients { get; set; }
    }
}

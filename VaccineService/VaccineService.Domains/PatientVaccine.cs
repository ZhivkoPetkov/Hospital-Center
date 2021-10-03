namespace VaccineService.Domains
{
    public class PatientVaccine
    {
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int VaccineId { get; set; }
        public virtual Vaccine Vaccine { get; set; }
    }
}

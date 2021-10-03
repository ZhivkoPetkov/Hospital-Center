using System.ComponentModel.DataAnnotations;

namespace PatientService.Domains
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}

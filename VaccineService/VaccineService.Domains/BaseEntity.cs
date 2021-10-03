using System.ComponentModel.DataAnnotations;

namespace VaccineService.Domains
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}

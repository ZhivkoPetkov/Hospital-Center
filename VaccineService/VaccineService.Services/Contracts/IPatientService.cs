using System.Collections.Generic;
using System.Threading.Tasks;
using VaccineService.Domains;
using VaccineService.Models.Patient;

namespace VaccineService.Services.Contracts
{
    public interface IPatientService
    {
        Patient GetById(int id);
        ICollection<Patient> All();
        Task<bool> AddPatient(Patient patient);
    }
}

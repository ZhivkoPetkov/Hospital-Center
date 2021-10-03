using System.Collections.Generic;
using VaccineService.Domains;

namespace VaccineService.Services.Contracts
{
    public interface IPatientService
    {
        Patient GetById(int id);
        ICollection<Patient> All();
    }
}

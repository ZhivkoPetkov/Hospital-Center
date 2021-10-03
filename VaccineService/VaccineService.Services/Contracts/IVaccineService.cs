using System.Collections.Generic;
using VaccineService.Domains;

namespace VaccineService.Services.Contracts
{
    public interface IVaccineService
    {
        void Add(Vaccine vaccine);
        Vaccine GetById(int id);
        ICollection<Vaccine> All();
        bool AddForPattient(int vaccineId, int patientId);
    }
}

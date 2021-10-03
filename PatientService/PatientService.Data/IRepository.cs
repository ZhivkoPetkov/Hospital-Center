using PatientService.Domains;
using System.Collections.Generic;

namespace PatientService.Data
{
    public interface IRepository
    {
        void Add(Patient entity);
        IEnumerable<Patient> GetAll();
        Patient GetByPatientId(int id);
        bool SaveChanges();
    }
}

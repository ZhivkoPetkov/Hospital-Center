using PatientService.Domains;
using System.Collections.Generic;
using System.Linq;

namespace PatientService.Data
{
    public class PatientRepository : IRepository
    {
        private readonly AppDbContext dbContext;

        public PatientRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Patient entity)
        {
            if (this.dbContext.Patients.Any(x => x.NAN == entity.NAN))
            {
                return;
            }
            this.dbContext.Patients.Add(entity);
        }

        public IEnumerable<Patient> GetAll()
            => this.dbContext.Patients.ToList();

        public Patient GetByPatientId(int id)
            => this.dbContext.Patients.FirstOrDefault(x => x.Id == id);

        public bool SaveChanges()
            => this.dbContext.SaveChanges() >= 0;

    }
}

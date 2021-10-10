using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccineService.Data;
using VaccineService.Domains;
using VaccineService.Services.Contracts;

namespace VaccineService.Services
{
    public class PattientService : IPatientService
    {
        private readonly AppDbContext dbContext;

        public PattientService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddPatient(Patient patient)
        {

            if (dbContext.Patients.Any(x => x.NAN == patient.NAN))
            {
                return false;
            }

            await this.dbContext.Patients.AddAsync(patient);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public ICollection<Patient> All()
            => this.dbContext.
            Patients.
            Include(g => g.Vaccnies).
            ToList();

        public Patient GetById(int id)
            => this.dbContext.
            Patients.
            Include(g => g.Vaccnies).
            FirstOrDefault(x => x.Id == id);
    }
}

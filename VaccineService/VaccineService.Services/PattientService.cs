using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
        public ICollection<Patient> All()
            => this.dbContext.Patients.Include(g => g.Vaccnies).ToList();

        public Patient GetById(int id)
            => this.dbContext.Patients.Include(g => g.Vaccnies).FirstOrDefault(x => x.Id == id);
    }
}

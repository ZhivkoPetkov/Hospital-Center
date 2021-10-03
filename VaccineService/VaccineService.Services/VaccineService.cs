using System.Collections.Generic;
using System.Linq;
using VaccineService.Data;
using VaccineService.Domains;
using VaccineService.Services.Contracts;

namespace VaccineService.Services
{
    public class VaccineService : IVaccineService
    {
        private readonly AppDbContext dbContext;
        private readonly IPatientService patientService;

        public VaccineService(AppDbContext dbContext, IPatientService patientService)
        {
            this.dbContext = dbContext;
            this.patientService = patientService;
        }
        public void Add(Vaccine vaccine)
        {
            this.dbContext.Vaccines.Add(vaccine);
            this.dbContext.SaveChanges();
        }

        public bool AddForPattient(int vaccineId, int patientId)
        {
            var vaccine = this.GetById(vaccineId);
            var pattient = this.patientService.GetById(patientId);

            if (vaccine is null || pattient is null)
            {
                return false;
            }

            this.dbContext.PatientVaccines.Add(new PatientVaccine() { PatientId = patientId, VaccineId = vaccineId });
            return this.dbContext.SaveChanges() > 0;
        }

        public ICollection<Vaccine> All()
            => this.dbContext.Vaccines.ToList();

        public Vaccine GetById(int id)
            => this.dbContext.Vaccines.FirstOrDefault(x => x.Id == id);
    }
}

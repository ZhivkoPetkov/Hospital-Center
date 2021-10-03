using System.Collections.Generic;
using System.Linq;
using VaccineService.Domains;

namespace VaccineService.Data
{
    public static class Seeder
    {
        public static void Seed(AppDbContext dbContext)
        {
            if (dbContext.Patients.Any() == false)
            {
                var vaccines = new List<Vaccine>()
                {
                    new Vaccine() { Id = 1, Name = "COVID-19 Vaccine", Description = "Moderna vaccine accepted by GOV 2020."},
                    new Vaccine() { Id = 2, Name = "HIV Vaccine", Description = "New vaccine from Norway doctors."},
                    new Vaccine() { Id = 3, Name = "Cancer Vaccine", Description = "New vaccine from Alien civilization."},
                };

                var patients = new List<Patient>()
                {
                    new Patient()
                    {
                        Id = 10000,
                        FirstName = "Lord",
                        LastName = "Raven",
                        City = "Madrid",
                        NAN = "42134231423"
                    },
                    new Patient()
                    {
                        Id = 10001,
                        FirstName = "Angel",
                        LastName = "Sofiisky",
                        City = "Sofia",
                        NAN = "31254325432"
                    },
                    new Patient()
                    {
                        Id = 10002,
                        FirstName = "Turtle",
                        LastName = "Faster",
                        City = "Alaska",
                        NAN = "4312432234"
                    }
                };
                dbContext.Patients.AddRange(patients);
                dbContext.Vaccines.AddRange(vaccines);
                dbContext.PatientVaccines.Add(new PatientVaccine() { PatientId = 10000, VaccineId = 1 });
                dbContext.SaveChanges();
            }
        }
    }
}

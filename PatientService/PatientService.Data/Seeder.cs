using PatientService.Domains;
using System.Collections.Generic;
using System.Linq;

namespace PatientService.Data
{
    public static class Seeder
    {
        public static void SeedPatients(AppDbContext dbContext)
        {
            if (dbContext.Patients.Any() == false)
            {
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
                dbContext.SaveChanges();
            }
        }
    }
}

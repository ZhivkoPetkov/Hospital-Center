using Microsoft.EntityFrameworkCore;
using VaccineService.Domains;

namespace VaccineService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<PatientVaccine> PatientVaccines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>().
                         Property(x => x.Id).
                         UseIdentityColumn(10000, 1);

            modelBuilder.Entity<PatientVaccine>().
                         HasKey(x => new { x.PatientId, x.VaccineId });

            modelBuilder.Entity<PatientVaccine>().
                         HasOne(p => p.Patient).
                         WithMany(g => g.Vaccnies).
                         HasForeignKey(x =>x.PatientId);

            modelBuilder.
                         Entity<PatientVaccine>().
                         HasOne(p => p.Vaccine).
                         WithMany(g => g.Patients).
                         HasForeignKey(v => v.VaccineId);
        }
    }
}

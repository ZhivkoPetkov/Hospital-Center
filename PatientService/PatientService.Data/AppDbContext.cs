using Microsoft.EntityFrameworkCore;
using PatientService.Domains;

namespace PatientService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>().
                         Property(x => x.Id).
                         UseIdentityColumn(10000, 1);
        }
    }
}

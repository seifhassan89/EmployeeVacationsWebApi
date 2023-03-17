using Data.Configurations;
using employee_task.Models;
using Microsoft.EntityFrameworkCore;

namespace employee_task.Data
{
    public class EmployeeDB : DbContext
    {
        public EmployeeDB(DbContextOptions<EmployeeDB> options) : base(options)
        {
        }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VacationBalance> VacationBalances { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Adds configurations for Student from separate class
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new VacationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }
    }
}

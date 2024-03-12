using Microsoft.EntityFrameworkCore;
using PhonebookManager.Models;

namespace PhonebookManager.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        //public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppClaim> AppClaims { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PhoneLine> PhoneLines { get; set; }
        public DbSet<PhoneUser> PhoneUsers { get; set; }
        public DbSet<ChangeRequest> ChangeRequests { get; set; }


    }
}

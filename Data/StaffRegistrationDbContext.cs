using Microsoft.EntityFrameworkCore;
using StaffRegistration.Models.Domain;

namespace StaffRegistration.Data
{
    public class StaffRegistrationDbContext : DbContext
    {
        public StaffRegistrationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Staff> Staff { get; set; }

    }
}

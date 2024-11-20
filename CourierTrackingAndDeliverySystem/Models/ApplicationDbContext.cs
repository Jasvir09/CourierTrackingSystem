using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CourierTrackingAndDeliverySystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
    }
}

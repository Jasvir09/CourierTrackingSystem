using CourierTrackingandDeliverySystem.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourierTrackingandDeliverySystem.Data
{
    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}


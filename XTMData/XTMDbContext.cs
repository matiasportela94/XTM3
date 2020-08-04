using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XTMCore;

namespace XTMData
{
    public class XTMDbContext : DbContext
    {

        public XTMDbContext(DbContextOptions<XTMDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avion>()
                .HasKey("PlaneID");
            modelBuilder.Entity<Booking>()
                .HasKey("BookingID");
            modelBuilder.Entity<Client>()
                .HasKey("UserID");
        }

        public DbSet<Avion> Planes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Persistance.Context
{
    public class ParkingManagementSystemContext: DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";



        public ParkingManagementSystemContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<ParkingRecord> ParkingRecords { get; set; }
        public DbSet<Region> Regions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(r => r.Name).IsRequired().HasMaxLength(50);
                entity.HasMany(r => r.ParkingRecords)
                      .WithOne(pr => pr.Region)
                      .HasForeignKey(pr => pr.RegionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ParkingRecord>(entity =>
            {
                entity.Property(pr => pr.VehicleSize).IsRequired();
                entity.Property(pr => pr.EntryTime).IsRequired();
            });
        }
    }
}


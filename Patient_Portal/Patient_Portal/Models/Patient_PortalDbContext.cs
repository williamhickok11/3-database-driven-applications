using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Patient_Portal.Models
{
    public class Patient_PortalDbContext : DbContext
    {
        public DbSet<PatientProfile> PatientProfile { get; set; }
        public DbSet<Ailment> Ailment { get; set; }
        public DbSet<AilmentPerscription> AilmentPerscription { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<MedPerscription> MedPerscription { get; set; }
        public DbSet<Procedure> Procedure { get; set; }
        public DbSet<ProcedurePrescription> ProcedurePrescription { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientProfile>()
                .ToTable("PatientProfile")
                .HasKey(pp => pp.IdPatientProfile);

            modelBuilder.Entity<Ailment>()
                .ToTable("Ailment")
                .HasKey(a => a.IdAilment);

            modelBuilder.Entity<AilmentPerscription>()
                .ToTable("AilmentPerscription")
                .HasKey(ap => ap.IdAilmentPerscription);

            modelBuilder.Entity<Medication>()
                .ToTable("Medication")
                .HasKey(m => m.IdMedication);

            modelBuilder.Entity<MedPerscription>()
                .ToTable("MedPerscription")
                .HasKey(mp => mp.IdMedPerscription);

            modelBuilder.Entity<Procedure>()
                .ToTable("Procedure")
                .HasKey(p => p.IdProcedure);

            modelBuilder.Entity<ProcedurePrescription>()
                .ToTable("ProcedurePrescription")
                .HasKey(ppres => ppres.IdProcedurePrescription);

        }
    }
}
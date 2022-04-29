using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BarcoPVG.Models.Db;
using BarcoDB_Admin.Dao;

namespace BarcoPVG.Dao
{
    public partial class BarcoContext : DbContext
    {
        public BarcoContext()
        {
        }

        public BarcoContext(DbContextOptions<BarcoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Eut> Euts { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<PlPlanning> PlPlannings { get; set; } = null!;
        public virtual DbSet<PlPlanningsKalender> PlPlanningsKalenders { get; set; } = null!;
        public virtual DbSet<PlResource> PlResources { get; set; } = null!;
        public virtual DbSet<PlResourcesDivision> PlResourcesDivisions { get; set; } = null!;
        public virtual DbSet<PlVerletdagen> PlVerletdagens { get; set; } = null!;
        public virtual DbSet<RqBarcoDivision> RqBarcoDivisions { get; set; } = null!;
        public virtual DbSet<RqBarcoDivisionPerson> RqBarcoDivisionPeople { get; set; } = null!;
        public virtual DbSet<RqJobNature> RqJobNatures { get; set; } = null!;
        public virtual DbSet<RqOptionel> RqOptionels { get; set; } = null!;
        public virtual DbSet<RqRequest> RqRequests { get; set; } = null!;
        public virtual DbSet<RqRequestDetail> RqRequestDetails { get; set; } = null!;
        public virtual DbSet<RqTestDevision> RqTestDevisions { get; set; } = null!;
        public virtual DbSet<Statistiek> Statistieks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SQLConnection.CONNECTION_STRING);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Eut>(entity =>
            {
                entity.ToTable("EUT");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvailableDate)
                    .HasColumnType("datetime")
                    .HasColumnName("available_date");

                entity.Property(e => e.IdRqDetail).HasColumnName("id_rq_detail");

                entity.Property(e => e.OmschrijvingEut)
                    .HasMaxLength(50)
                    .HasColumnName("omschrijvingEUT");

                entity.HasOne(d => d.IdRqDetailNavigation)
                    .WithMany(p => p.Euts)
                    .HasForeignKey(d => d.IdRqDetail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EUT_Rq_RequestDetail_FK");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Afkorting)
                    .HasName("PersonTbl_PK");

                entity.ToTable("Person");

                entity.Property(e => e.Afkorting).HasMaxLength(10);

                entity.Property(e => e.Familienaam).HasMaxLength(50);

                entity.Property(e => e.Voornaam).HasMaxLength(50);

                entity.Property(e => e.wachtwoord).HasMaxLength(50);
            });

            modelBuilder.Entity<PlPlanning>(entity =>
            {
                entity.HasKey(e => e.IdPlanning)
                    .HasName("planning_PK");

                entity.ToTable("Pl_planning");

                entity.Property(e => e.IdPlanning)
                    .ValueGeneratedNever()
                    .HasColumnName("id_planning");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.IdRequest).HasColumnName("id_request");

                entity.Property(e => e.JrNr)
                    .HasMaxLength(10)
                    .HasColumnName("JR_Nr");

                entity.Property(e => e.Requestdate)
                    .HasColumnType("date")
                    .HasColumnName("requestdate");

                entity.Property(e => e.TestDiv)
                    .HasMaxLength(4)
                    .HasColumnName("testDiv");

                entity.Property(e => e.TestDivPlanDate)
                    .HasColumnType("date")
                    .HasColumnName("testDiv_planDate");

                entity.Property(e => e.TestDivStatus)
                    .HasMaxLength(20)
                    .HasColumnName("testDiv_status");

                entity.HasOne(d => d.IdRequestNavigation)
                    .WithMany(p => p.PlPlannings)
                    .HasForeignKey(d => d.IdRequest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("planning_Rq_Request_FK");
            });

            modelBuilder.Entity<PlPlanningsKalender>(entity =>
            {
                entity.ToTable("Pl_planningsKalender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Einddatum)
                    .HasColumnType("date")
                    .HasColumnName("einddatum");

                entity.Property(e => e.IdRequest).HasColumnName("id_request");

                entity.Property(e => e.JrNr)
                    .HasMaxLength(10)
                    .HasColumnName("JR_nr");

                entity.Property(e => e.JrStatus)
                    .HasMaxLength(20)
                    .HasColumnName("JR_status");

                entity.Property(e => e.Omschrijving)
                    .HasMaxLength(150)
                    .HasColumnName("omschrijving");

                entity.Property(e => e.Reserve)
                    .HasMaxLength(100)
                    .HasColumnName("reserve?");

                entity.Property(e => e.Resources).HasColumnName("resources");

                entity.Property(e => e.Startdatum)
                    .HasColumnType("date")
                    .HasColumnName("startdatum");

                entity.Property(e => e.TestStatus)
                    .HasMaxLength(20)
                    .HasColumnName("test_status");

                entity.Property(e => e.Testdiv)
                    .HasMaxLength(5)
                    .HasColumnName("testdiv");

                entity.HasOne(d => d.IdRequestNavigation)
                    .WithMany(p => p.PlPlanningsKalenders)
                    .HasForeignKey(d => d.IdRequest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("planningsKalender_Rq_Request_FK");

                entity.HasOne(d => d.ResourcesNavigation)
                    .WithMany(p => p.PlPlanningsKalenders)
                    .HasForeignKey(d => d.Resources)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("planningsKalender_Resources_FK");
            });

            modelBuilder.Entity<PlResource>(entity =>
            {
                entity.ToTable("Pl_Resources");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KleurHex)
                    .HasMaxLength(8)
                    .HasColumnName("kleur-hex");

                entity.Property(e => e.KleurRgb)
                    .HasMaxLength(11)
                    .HasColumnName("kleur - rgb");

                entity.Property(e => e.Naam)
                    .HasMaxLength(50)
                    .HasColumnName("naam");
            });

            modelBuilder.Entity<PlResourcesDivision>(entity =>
            {
                entity.ToTable("Pl_ResourcesDivision");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DivisionAfkorting)
                    .HasMaxLength(4)
                    .HasColumnName("Division_Afkorting");

                entity.Property(e => e.ResourcesId).HasColumnName("Resources_ID");

                entity.HasOne(d => d.DivisionAfkortingNavigation)
                    .WithMany(p => p.PlResourcesDivisions)
                    .HasForeignKey(d => d.DivisionAfkorting)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ResourcesDivision_Rq_TestDevision_FK");

                entity.HasOne(d => d.Resources)
                    .WithMany(p => p.PlResourcesDivisions)
                    .HasForeignKey(d => d.ResourcesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ResourcesDivision_Resources_FK");
            });

            modelBuilder.Entity<PlVerletdagen>(entity =>
            {
                entity.HasKey(e => e.Datum)
                    .HasName("Verletdagen_PK");

                entity.ToTable("Pl_Verletdagen");

                entity.Property(e => e.Datum)
                    .HasColumnType("date")
                    .HasColumnName("datum");

                entity.Property(e => e.Omschrijving)
                    .HasMaxLength(30)
                    .HasColumnName("omschrijving");
            });

            modelBuilder.Entity<RqBarcoDivision>(entity =>
            {
                entity.HasKey(e => e.Afkorting)
                    .HasName("BarcoDivision");

                entity.ToTable("Rq_BarcoDivision");

                entity.Property(e => e.Afkorting)
                    .HasMaxLength(50)
                    .HasColumnName("afkorting");

                entity.Property(e => e.Actief).HasColumnName("actief");

                entity.Property(e => e.Alias)
                    .HasMaxLength(50)
                    .HasColumnName("alias");
            });

            modelBuilder.Entity<RqBarcoDivisionPerson>(entity =>
            {
                entity.ToTable("Rq_BarcoDivisionPerson");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AfkDevision)
                    .HasMaxLength(50)
                    .HasColumnName("afkDevision");

                entity.Property(e => e.AfkPerson)
                    .HasMaxLength(10)
                    .HasColumnName("afkPerson");

                entity.Property(e => e.Pvggroup)
                    .HasMaxLength(10)
                    .HasColumnName("PVGGroup");
            });

            modelBuilder.Entity<RqJobNature>(entity =>
            {
                entity.HasKey(e => e.Nature)
                    .HasName("JobNature_PK");

                entity.ToTable("Rq_JobNature");

                entity.Property(e => e.Nature)
                    .HasMaxLength(20)
                    .HasColumnName("nature");
            });

            modelBuilder.Entity<RqOptionel>(entity =>
            {
                entity.ToTable("Rq_Optionel");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdRequest).HasColumnName("id_request");

                entity.Property(e => e.Link)
                    .HasMaxLength(500)
                    .HasColumnName("link");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(1000)
                    .HasColumnName("remarks");

                entity.HasOne(d => d.IdRequestNavigation)
                    .WithMany(p => p.RqOptionels)
                    .HasForeignKey(d => d.IdRequest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Rq_Optionel_Rq_Request_FK");
            });

            modelBuilder.Entity<RqRequest>(entity =>
            {
                entity.HasKey(e => e.IdRequest)
                    .HasName("RequestTbl_PK");

                entity.ToTable("Rq_Request");

                entity.Property(e => e.IdRequest).HasColumnName("id_request");

                entity.Property(e => e.BarcoDivision)
                    .HasMaxLength(25)
                    .HasColumnName("barcoDivision")
                    .HasComment("uit keuzelijst");

                entity.Property(e => e.Battery).HasColumnName("battery");

                entity.Property(e => e.EutPartnumbers)
                    .HasMaxLength(500)
                    .HasColumnName("EUT_Partnumbers");

                entity.Property(e => e.EutProjectname)
                    .HasMaxLength(100)
                    .HasColumnName("EUT_Projectname");

                entity.Property(e => e.ExpectedEnddate)
                    .HasColumnType("datetime")
                    .HasColumnName("expectedEnddate");

                entity.Property(e => e.GrossWeight)
                    .HasMaxLength(200)
                    .HasColumnName("grossWeight");

                entity.Property(e => e.HydraProjectNr)
                    .HasMaxLength(15)
                    .HasColumnName("hydraProjectNr");

                entity.Property(e => e.JobNature)
                    .HasMaxLength(30)
                    .HasColumnName("jobNature");

                entity.Property(e => e.JrNumber)
                    .HasMaxLength(10)
                    .HasColumnName("JR_Number");

                entity.Property(e => e.JrStatus)
                    .HasMaxLength(30)
                    .HasColumnName("JR_Status");

                entity.Property(e => e.NetWeight)
                    .HasMaxLength(200)
                    .HasColumnName("netWeight");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("datetime")
                    .HasColumnName("request_date");

                entity.Property(e => e.Requester)
                    .HasMaxLength(10)
                    .HasColumnName("requester")
                    .HasComment("initialen");
            });

            modelBuilder.Entity<RqRequestDetail>(entity =>
            {
                entity.HasKey(e => e.IdRqDetail)
                    .HasName("NatureOfTest_PK");

                entity.ToTable("Rq_RequestDetail");

                entity.Property(e => e.IdRqDetail).HasColumnName("id_rq_detail");

                entity.Property(e => e.IdRequest).HasColumnName("id_request");

                entity.Property(e => e.Pvgresp)
                    .HasMaxLength(30)
                    .HasColumnName("PVGresp");

                entity.Property(e => e.Testdivisie)
                    .HasMaxLength(4)
                    .HasColumnName("testdivisie");

                entity.HasOne(d => d.IdRequestNavigation)
                    .WithMany(p => p.RqRequestDetails)
                    .HasForeignKey(d => d.IdRequest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Rq_RequestDetail_Rq_Request_FK");

                entity.HasOne(d => d.TestdivisieNavigation)
                    .WithMany(p => p.RqRequestDetails)
                    .HasForeignKey(d => d.Testdivisie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Rq_RequestDetail_Rq_TestDevision_FK");
            });

            modelBuilder.Entity<RqTestDevision>(entity =>
            {
                entity.HasKey(e => e.Afkorting)
                    .HasName("Test_Devisionv1_PK");

                entity.ToTable("Rq_TestDevision");

                entity.Property(e => e.Afkorting)
                    .HasMaxLength(4)
                    .HasColumnName("afkorting");

                entity.Property(e => e.Naam)
                    .HasMaxLength(50)
                    .HasColumnName("naam");
            });

            modelBuilder.Entity<Statistiek>(entity =>
            {
                entity.ToTable("statistiek");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.JrNr)
                    .HasMaxLength(10)
                    .HasColumnName("JR_nr");

                entity.Property(e => e.Maand).HasColumnName("maand");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

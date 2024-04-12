using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Modelcasepro.Entities;

public partial class CaseproDbContext : DbContext
{
    public CaseproDbContext()
    {
    }

    public CaseproDbContext(DbContextOptions<CaseproDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityCustomerTable> ActivityCustomerTables { get; set; }

    public virtual DbSet<ActivityDetail> ActivityDetails { get; set; }

    public virtual DbSet<ActivityImage> ActivityImages { get; set; }

    public virtual DbSet<ActivityNote> ActivityNotes { get; set; }

    public virtual DbSet<ActivityProductDetail> ActivityProductDetails { get; set; }

    public virtual DbSet<ActivityResourcesDetail> ActivityResourcesDetails { get; set; }

    public virtual DbSet<ActivitySignOffdetail> ActivitySignOffdetails { get; set; }

    public virtual DbSet<ActivityStatusTable> ActivityStatusTables { get; set; }

    public virtual DbSet<ActivityTable> ActivityTables { get; set; }

    public virtual DbSet<ActivityTrailerTable> ActivityTrailerTables { get; set; }

    public virtual DbSet<InstructorFormDetail> InstructorFormDetails { get; set; }

    public virtual DbSet<InstructorName> InstructorNames { get; set; }

    public virtual DbSet<UsersTable> UsersTables { get; set; }
  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityCustomerTable>(entity =>
        {
            entity.ToTable("Activity_Customer_Table");

            entity.Property(e => e.ActivityId).HasColumnName("activityId");
            entity.Property(e => e.AddressLine1).HasColumnName("Address_Line1");
            entity.Property(e => e.AddressLine2).HasColumnName("Address_Line2");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.ContactNo).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Postcode).HasMaxLength(50);

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityCustomerTables)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_Activity_Customer_Table_Activity_Table");
        });

        modelBuilder.Entity<ActivityDetail>(entity =>
        {
            entity.ToTable("ActivityDetail");

            entity.Property(e => e.ActivityDate).HasColumnType("datetime");
            entity.Property(e => e.AllRelevantActivityRams)
                .HasMaxLength(50)
                .HasColumnName("AllRelevantActivityRAMS");
            entity.Property(e => e.AnchoringDetails).HasMaxLength(50);
            entity.Property(e => e.AnyNearMissOccurrences).HasMaxLength(50);
            entity.Property(e => e.AnySpecialInstructions).HasMaxLength(50);
            entity.Property(e => e.BarrierConditionChecks).HasMaxLength(50);
            entity.Property(e => e.BarrierPerformance)
                .HasMaxLength(50)
                .HasColumnName("Barrier_Performance");
            entity.Property(e => e.BarrierQty)
                .HasMaxLength(50)
                .HasColumnName("Barrier_Qty");
            entity.Property(e => e.BarrierStartAndFinishLocation).HasMaxLength(50);
            entity.Property(e => e.BarrierType).HasMaxLength(50);
            entity.Property(e => e.ChainLiftingequipmenttobeused).HasMaxLength(50);
            entity.Property(e => e.IncidentReporting).HasMaxLength(50);
            entity.Property(e => e.Isapermittobreakgroundrequired).HasMaxLength(50);
            entity.Property(e => e.LabourSupplier).HasMaxLength(50);
            entity.Property(e => e.LengthOfRuns).HasMaxLength(50);
            entity.Property(e => e.LiftingEquipmentUsed).HasMaxLength(50);
            entity.Property(e => e.MeetingSite).HasMaxLength(50);
            entity.Property(e => e.NoOfPersoneSupplied)
                .HasMaxLength(50)
                .HasColumnName("No_of_Persone_Supplied");
            entity.Property(e => e.OtherResourcesEquipmentUsed).HasMaxLength(50);
            entity.Property(e => e.Startandfinishtime).HasMaxLength(50);
            entity.Property(e => e.SupplierContact).HasMaxLength(50);

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityDetails)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_ActivityDetail_ActivityTable");
        });

        modelBuilder.Entity<ActivityImage>(entity =>
        {
            entity.Property(e => e.ImageName).HasMaxLength(50);
            entity.Property(e => e.UploadedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityImages)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_ActivityImages_ActivityTable");
        });

        modelBuilder.Entity<ActivityNote>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityId).HasColumnName("activityId");
            entity.Property(e => e.Notes).IsUnicode(false);

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityNotes)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_ActivityNotes_ActivityTable");
        });

        modelBuilder.Entity<ActivityProductDetail>(entity =>
        {
            entity.ToTable("ActivityProduct_Details");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Shift).HasMaxLength(50);
            entity.Property(e => e.SummaryOfWorks).HasMaxLength(50);

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityProductDetails)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_Productdetails_Activityid");
        });

        modelBuilder.Entity<ActivityResourcesDetail>(entity =>
        {
            entity.ToTable("ActivityResources_Details");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.DayNight)
                .HasMaxLength(50)
                .HasColumnName("Day_night");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Shift).HasMaxLength(50);

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityResourcesDetails)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_ResourcesDetails_ActivityId");
        });

        modelBuilder.Entity<ActivitySignOffdetail>(entity =>
        {
            entity.ToTable("Activity_SignOffdetail");

            entity.Property(e => e.ActivityId).HasColumnName("activityId");
            entity.Property(e => e.CompetionDate).HasColumnType("date");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.PrintName).HasMaxLength(50);
            entity.Property(e => e.Signature).HasMaxLength(50);

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivitySignOffdetails)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_Activity_SignOffdetail_Activity_Table");
        });

        modelBuilder.Entity<ActivityStatusTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Activity_Status_Code");

            entity.ToTable("Activity_Status_Table");

            entity.Property(e => e.ActivityId).HasColumnName("activityId");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityStatusTables)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_Activity_Status_Table_Activity_Table");
        });

        modelBuilder.Entity<ActivityTable>(entity =>
        {
            entity.ToTable("Activity_Table");

            entity.Property(e => e.ActivitType)
                .HasMaxLength(50)
                .HasColumnName("Activit_Type");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.CustomerOrderNumber)
                .HasMaxLength(50)
                .HasColumnName("CustomerOrder_Number");
            entity.Property(e => e.DateRaised)
                .HasMaxLength(50)
                .HasColumnName("Date_Raised");
            entity.Property(e => e.HcorderNumber)
                .HasMaxLength(50)
                .HasColumnName("HCorder_Number");
            entity.Property(e => e.NearestAE)
                .HasMaxLength(50)
                .HasColumnName("Nearest_A_E");
            entity.Property(e => e.OutorhoursEmrgContact)
                .HasMaxLength(50)
                .HasColumnName("Outorhours_Emrg_Contact");
            entity.Property(e => e.RaisedBy)
                .HasMaxLength(50)
                .HasColumnName("Raised_By");
            entity.Property(e => e.SageOrderNumber)
                .HasMaxLength(50)
                .HasColumnName("SageOrder_Number");
            entity.Property(e => e.SignOffId).HasColumnName("signOffId");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityTables)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Activity_Userid");
        });

        modelBuilder.Entity<ActivityTrailerTable>(entity =>
        {
            entity.ToTable("Activity_Trailer_Table");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.DepartFrom).HasMaxLength(50);
            entity.Property(e => e.LoadDepot).HasMaxLength(50);
            entity.Property(e => e.LoadedTippedBy).HasMaxLength(50);
            entity.Property(e => e.Loadpositioned).HasMaxLength(50);
            entity.Property(e => e.Quantity).HasMaxLength(50);
            entity.Property(e => e.TrailerNumber).HasMaxLength(50);
            entity.Property(e => e.TrailerSupplier).HasMaxLength(50);
            entity.Property(e => e.VehicleReg).HasMaxLength(50);

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityTrailerTables)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK_Activity_Trailer_Table_Activity_Table");
        });

        modelBuilder.Entity<InstructorFormDetail>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Note).IsUnicode(false);
            entity.Property(e => e.SelectedActivity).HasMaxLength(50);
        });

        modelBuilder.Entity<InstructorName>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UsersTable>(entity =>
        {
            entity.ToTable("Users_table");

            entity.Property(e => e.ClientName).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.LastLogindate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

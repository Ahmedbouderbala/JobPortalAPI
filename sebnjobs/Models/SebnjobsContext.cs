using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sebnjobs.Models;

public partial class SebnjobsContext : DbContext
{
    public SebnjobsContext()
    {
    }

    public SebnjobsContext(DbContextOptions<SebnjobsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Job1> Jobs1 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=FEJWPC0200\\LOCALDB;Initial Catalog=sebnjobs;Persist Security Info=False;User ID=sa;Password=Ahmad123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_Employees");

            entity.ToTable("Employee", "Emp");

            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK_Config_Job");

            entity.ToTable("Job", "Config");

            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Title).IsUnicode(false);
        });

        modelBuilder.Entity<Job1>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.JobId }).HasName("PK_Emp_Jobs");

            entity.ToTable("Job", "Emp");

            entity.Property(e => e.Description).IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Job1s)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Emp_Jobs_Employee");

            entity.HasOne(d => d.Job).WithMany(p => p.Job1s)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Emp_Jobs_Config_Job");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

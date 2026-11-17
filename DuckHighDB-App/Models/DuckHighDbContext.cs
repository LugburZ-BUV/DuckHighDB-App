using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DuckHighDB_App.Models;

public partial class DuckHighDbContext : DbContext
{
    public DuckHighDbContext()
    {
    }

    public DuckHighDbContext(DbContextOptions<DuckHighDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubjectAssignment> SubjectAssignments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927A04184E9B5");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.MentorId).HasColumnName("MentorID");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SchoolYear)
                .HasMaxLength(4)
                .IsUnicode(false);

            entity.HasOne(d => d.Mentor).WithMany(p => p.Classes)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("FK__Classes__MentorI__5BE2A6F2");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD7BF99BAF");

            entity.HasIndex(e => e.Name, "UQ__Departme__737584F65CE202DF").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF10ED26D6D");

            entity.HasIndex(e => e.Ssn, "UQ__Employee__CA1E8E3CDFBDA4DB").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Ssn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("SSN");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Depar__52593CB8");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Depar__5165187F");
        });

        modelBuilder.Entity<EmployeePosition>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Employee__60BB9A595C56308B");

            entity.HasIndex(e => e.Name, "UQ__Employee__737584F6FC614704").IsUnique();

            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F6877FBF4D79999");

            entity.ToTable("Enrollment");

            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Class).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__Class__6477ECF3");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__Stude__6383C8BA");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__54F87A37DA748D57");

            entity.Property(e => e.GradeId).HasColumnName("GradeID");
            entity.Property(e => e.Grade1)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Grade");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__StudentI__6754599E");

            entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__SubjectI__68487DD7");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Grades)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__TeacherI__693CA210");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A79733AE0AC");

            entity.HasIndex(e => e.Email, "UQ__Students__A9D105342A259691").IsUnique();

            entity.HasIndex(e => e.Ssn, "UQ__Students__CA1E8E3C8CB56AC1").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Ssn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("SSN");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA3889C418598");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SubjectAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__SubjectA__32499E5734D12D08");

            entity.ToTable("SubjectAssignment");

            entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.SchoolYear)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Class).WithMany(p => p.SubjectAssignments)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectAs__Class__5EBF139D");

            entity.HasOne(d => d.Subject).WithMany(p => p.SubjectAssignments)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectAs__Subje__5FB337D6");

            entity.HasOne(d => d.Teacher).WithMany(p => p.SubjectAssignments)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectAs__Teach__60A75C0F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

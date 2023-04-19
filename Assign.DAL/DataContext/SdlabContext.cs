using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Assign.DAL.Models;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Assign.DAL.DataContext;

public partial class SdlabContext : DbContext
{
    public SdlabContext()
    {
    }

    public SdlabContext(DbContextOptions<SdlabContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Lab> Labs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresBoi"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Asid).HasName("assignments_pkey");

            entity.ToTable("assignments");

            entity.HasIndex(e => e.AsDesc, "assignments_as_desc_key").IsUnique();

            entity.HasIndex(e => e.AsName, "assignments_as_name_key").IsUnique();

            entity.Property(e => e.Asid).HasColumnName("asid");
            entity.Property(e => e.AsDesc)
                .HasMaxLength(300)
                .HasColumnName("as_desc");
            entity.Property(e => e.AsName)
                .HasMaxLength(30)
                .HasColumnName("as_name");
            entity.Property(e => e.Deadline).HasColumnName("deadline");
            entity.Property(e => e.Lid).HasColumnName("lid");

            entity.HasOne(d => d.LidNavigation).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Lid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("assignments_lid_fkey");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttId).HasName("attendance_pkey");

            entity.ToTable("attendance");

            entity.Property(e => e.AttId)
                .HasDefaultValueSql("nextval('attendance_aid_seq'::regclass)")
                .HasColumnName("att_id");
            entity.Property(e => e.LabId).HasColumnName("lab_id");
            entity.Property(e => e.Present).HasColumnName("present");
            entity.Property(e => e.StudId).HasColumnName("stud_id");

            entity.HasOne(d => d.Lab).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.LabId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("attendance_lid_fkey");

            entity.HasOne(d => d.Stud).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.StudId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("attendance_sid_fkey");
        });

        modelBuilder.Entity<Lab>(entity =>
        {
            entity.HasKey(e => e.Lid).HasName("labs_pkey");

            entity.ToTable("labs");

            entity.HasIndex(e => e.Date, "labs_date_key").IsUnique();

            entity.HasIndex(e => e.Description, "labs_description_key").IsUnique();

            entity.HasIndex(e => e.LabNo, "labs_lab_no_key").IsUnique();

            entity.HasIndex(e => e.Title, "labs_title_key").IsUnique();

            entity.Property(e => e.Lid).HasColumnName("lid");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.LabNo).HasColumnName("lab_no");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Sid).HasName("students_pkey");

            entity.ToTable("students");

            entity.HasIndex(e => e.Email, "students_email_key").IsUnique();

            entity.HasIndex(e => e.Name, "students_name_key").IsUnique();

            entity.HasIndex(e => e.Password, "students_password_key").IsUnique();

            entity.Property(e => e.Sid).HasColumnName("sid");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.GroupNo).HasColumnName("group_no");
            entity.Property(e => e.Hobby)
                .HasMaxLength(50)
                .HasColumnName("hobby");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.SubId).HasName("grades_pkey");

            entity.ToTable("submissions");

            entity.Property(e => e.SubId)
                .HasDefaultValueSql("nextval('grades_gid_seq'::regclass)")
                .HasColumnName("sub_id");
            entity.Property(e => e.AsignId).HasColumnName("asign_id");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.Link)
                .HasMaxLength(50)
                .HasColumnName("link");
            entity.Property(e => e.StudId).HasColumnName("stud_id");

            entity.HasOne(d => d.Asign).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.AsignId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("grades_asid_fkey");

            entity.HasOne(d => d.Stud).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.StudId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("grades_sid_fkey");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Tid).HasName("teachers_pkey");

            entity.ToTable("Teacher");

            entity.HasIndex(e => e.Email, "teachers_email_key").IsUnique();

            entity.HasIndex(e => e.Name, "teachers_name_key").IsUnique();

            entity.HasIndex(e => e.Password, "teachers_password_key").IsUnique();

            entity.Property(e => e.Tid)
                .HasDefaultValueSql("nextval('teachers_tid_seq'::regclass)")
                .HasColumnName("tid");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e=> e.TokId).HasName("tokens_pkey");
            entity.ToTable("tokens");

            entity.Property(e => e.TokId)
                .HasDefaultValueSql("nextval('\"Subjects_Sub_id_seq\"'::regclass)")
                .HasColumnName("tok_id");
            entity.Property(e => e.Token1).HasColumnName("token");
            entity.Property(e=> e.Used).HasColumnName("used");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

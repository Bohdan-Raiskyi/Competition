using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CompetitionDomain.Model;

namespace CompetitionInfrastructure;

public partial class SwimmingCompetitionDbContext : DbContext
{
    public SwimmingCompetitionDbContext()
    {
    }

    public SwimmingCompetitionDbContext(DbContextOptions<SwimmingCompetitionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<Distance> Distances { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Swim> Swims { get; set; }

    public virtual DbSet<Swimmer> Swimmers { get; set; }

    public virtual DbSet<SqlQuery> SqlQuery { get; set; }


    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=Bogdans_PC\\SQLEXPRESS; Database=SwimmingCompetitionDB; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("Server=Bogdans_PC\\SQLEXPRESS; Database=SwimmingCompetitionDB; Trusted_Connection=True; TrustServerCertificate=True; ")
            .LogTo(Console.WriteLine, LogLevel.Information) // Виводить запити у консоль
            .EnableSensitiveDataLogging(); // Показує значення параметрів
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3213E83F34E604D5");

            entity.ToTable("Competition");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.TimedTo)
                .HasMaxLength(100)
                .HasColumnName("timed_to");
            entity.Property(e => e.Venue)
                .HasMaxLength(100)
                .HasColumnName("venue");
        });

        modelBuilder.Entity<Distance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Distance__3213E83FE97ED978");

            entity.ToTable("Distance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Style)
                .HasMaxLength(50)
                .HasColumnName("style");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Results__3213E83FF9D9027B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArrivalTime)
                .HasPrecision(2)
                .HasColumnName("arrival_time");
            entity.Property(e => e.Place).HasColumnName("place");
            entity.Property(e => e.ResultTime)
                .HasPrecision(2)
                .HasColumnName("result_time");
            entity.Property(e => e.SwimId).HasColumnName("swim_id");
            entity.Property(e => e.SwimmerId).HasColumnName("swimmer_id");

            entity.HasOne(d => d.Swim).WithMany(p => p.Results)
                .HasForeignKey(d => d.SwimId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Results__swim_id__412EB0B6");

            entity.HasOne(d => d.Swimmer).WithMany(p => p.Results)
                .HasForeignKey(d => d.SwimmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Results__swimmer__4222D4EF");
        });

        modelBuilder.Entity<Swim>(entity =>
        {

            entity.HasKey(e => e.Id).HasName("PK__Swim__3213E83F3C8E0F44");

            entity.ToTable("Swim");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.DistanceId).HasColumnName("distance_id");
            entity.Property(e => e.StartTime)
                .HasPrecision(2)
                .HasColumnName("start_time");
            entity.Property(e => e.SwimNumber).HasColumnName("swim_number");

            entity.HasOne(d => d.Competition).WithMany(p => p.Swims)
                .HasForeignKey(d => d.CompetitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Swim__competitio__3E52440B");

            entity.HasOne(d => d.Distance).WithMany(p => p.Swims)
                .HasForeignKey(d => d.DistanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Swim__distance_i__3D5E1FD2");
        });

        modelBuilder.Entity<Swimmer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Swimmer__3213E83FB097CCBC");

            entity.ToTable("Swimmer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgeCategory)
                .HasMaxLength(50)
                .HasColumnName("age_category");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .HasColumnName("team_name");
        });

        // Налаштування для SqlQuery
        modelBuilder.Entity<SqlQuery>(entity =>
        {
            entity.ToTable("SqlQuery");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(e => e.QueryText)
                .IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

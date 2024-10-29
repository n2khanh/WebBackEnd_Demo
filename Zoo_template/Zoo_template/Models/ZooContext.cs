using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Zoo_template.Models;

public partial class ZooContext : DbContext
{
    public ZooContext()
    {
    }

    public ZooContext(DbContextOptions<ZooContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TAnimal> TAnimals { get; set; }

    public virtual DbSet<TArea> TAreas { get; set; }

    public virtual DbSet<TCage> TCages { get; set; }

    public virtual DbSet<TEmployee> TEmployees { get; set; }

    public virtual DbSet<TFood> TFoods { get; set; }

    public virtual DbSet<TShift> TShifts { get; set; }

    public virtual DbSet<TSpecy> TSpecies { get; set; }

    public virtual DbSet<TLogin> TLogin { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("Data Source=DESKTOP-A3NO6EJ\\SQLEXPRESS;Initial Catalog=QLVuonThu_Web1;User ID=sa;Password=123456789;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TAnimal>(entity =>
        {
            entity.HasKey(e => e.AnimalId).HasName("PK__tAnimal__A21A73279E0A61EA");

            entity.Property(e => e.AnimalId).ValueGeneratedNever();

            entity.HasOne(d => d.Cage).WithMany(p => p.TAnimals).HasConstraintName("FK_tAnimal_tCage");

            entity.HasOne(d => d.Food).WithMany(p => p.TAnimals).HasConstraintName("FK_tAnimal_tFood");

            entity.HasOne(d => d.Species).WithMany(p => p.TAnimals).HasConstraintName("FK_tAnimal_tSpecies");
        });

        modelBuilder.Entity<TArea>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("PK__tArea__70B820284FBF46BE");

            entity.Property(e => e.AreaId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TCage>(entity =>
        {
            entity.HasKey(e => e.CageId).HasName("PK__tCage__792D9FBA2976E9B6");

            entity.Property(e => e.CageId).ValueGeneratedNever();

            entity.HasOne(d => d.Area).WithMany(p => p.TCages).HasConstraintName("FK_tCage_tArea");
        });

        modelBuilder.Entity<TEmployee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__tEmploye__7AD04FF1685A20D1");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();

            entity.HasOne(d => d.ResAreaNavigation).WithMany(p => p.TEmployees).HasConstraintName("FK_tEmployee_tArea");

            entity.HasOne(d => d.Shift).WithMany(p => p.TEmployees).HasConstraintName("FK_tEmployee_tShift");
        });

        modelBuilder.Entity<TFood>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("PK__tFood__856DB3CBAD5EEB77");

            entity.Property(e => e.FoodId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TShift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("PK__tShift__C0A838E18F7F9B88");

            entity.Property(e => e.ShiftId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TSpecy>(entity =>
        {
            entity.HasKey(e => e.SpeciesId).HasName("PK__tSpecies__A938047FFE645482");

            entity.Property(e => e.SpeciesId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TLogin>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK_TLogin");
            entity.Property(e => e.UserName).ValueGeneratedNever();
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Projektni_zadatak___RWA.Models;

public partial class TorterieMacaronContext : DbContext
{
    public TorterieMacaronContext()
    {
    }

    public TorterieMacaronContext(DbContextOptions<TorterieMacaronContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Korisnici> Korisnicis { get; set; }

    public virtual DbSet<Korpa> Korpas { get; set; }

    public virtual DbSet<Proizvodi> Proizvodis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-GFH6RA1\\SQLEXPRESS;Database=TorterieMacaron;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Korisnici>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Korisnic__3214EC076556EAA4");

            entity.ToTable("Korisnici");

            entity.HasIndex(e => e.Email, "UQ__Korisnic__A9D1053452DA1B3F").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Lozinka).HasMaxLength(100);
        });

        modelBuilder.Entity<Korpa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Korpa__3214EC07E74669DB");

            entity.ToTable("Korpa");

            entity.Property(e => e.Kolicina).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.Korpas)
                .HasForeignKey(d => d.ProizvodId)
                .HasConstraintName("FK__Korpa__ProizvodI__5EBF139D");
        });

        modelBuilder.Entity<Proizvodi>(entity =>
        {
            entity.ToTable("Proizvodi");

            entity.Property(e => e.Cijena).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Naziv).HasMaxLength(50);
            entity.Property(e => e.Opis).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

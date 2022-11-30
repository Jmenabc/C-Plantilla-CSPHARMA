using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Modelos;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DlkCatAccEmpleado> DlkCatAccEmpleados { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<DlkCatAccEmpleado>(entity =>
        {
            entity.HasKey(e => e.CodEmpleado).HasName("dlk_cat_acc_empleado_pkey");

            entity.ToTable("dlk_cat_acc_empleado");

            entity.Property(e => e.CodEmpleado)
                .ValueGeneratedNever()
                .HasColumnName("cod_empleado");
            entity.Property(e => e.ClaveEmpleado)
                .HasColumnType("character varying")
                .HasColumnName("clave_empleado");
            entity.Property(e => e.MdDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("md_date");
            entity.Property(e => e.MdUuid)
                .HasColumnType("character varying")
                .HasColumnName("md_uuid");
            entity.Property(e => e.NivelAccesoEmpleado).HasColumnName("nivel_acceso_empleado");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Isadmin).HasColumnName("isadmin");
            entity.Property(e => e.UsuarioNick)
                .HasColumnType("character varying")
                .HasColumnName("usuario_nick");
            entity.Property(e => e.UsuarioPassword)
                .HasColumnType("character varying")
                .HasColumnName("usuario_password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

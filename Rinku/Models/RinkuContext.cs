﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Rinku.Models
{
    public partial class RinkuContext : DbContext
    {
        public RinkuContext()
        {
        }

        public RinkuContext(DbContextOptions<RinkuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Movimientos> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleados>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("numero");

                entity.Property(e => e.Rol).HasColumnName("rol");
            });

            modelBuilder.Entity<Movimientos>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CantidadEntregas).HasColumnName("cantidadEntregas");

                entity.Property(e => e.Isr)
                    .HasColumnType("money")
                    .HasColumnName("isr");

                entity.Property(e => e.IsrAdicional)
                    .HasColumnType("money")
                    .HasColumnName("isrAdicional");

                entity.Property(e => e.Mes).HasColumnName("mes");

                entity.Property(e => e.NumEmpleado)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("numEmpleado");

                entity.Property(e => e.SueldoBruto)
                    .HasColumnType("money")
                    .HasColumnName("sueldoBruto");

                entity.Property(e => e.SueldoNeto)
                    .HasColumnType("money")
                    .HasColumnName("sueldoNeto");

                entity.Property(e => e.Vales)
                    .HasColumnType("money")
                    .HasColumnName("vales");
            });

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
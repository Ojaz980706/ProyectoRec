using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MVCTemplate.Models.ModelosCamara
{
    public partial class Modelo : DbContext
    {
        public Modelo()
            : base("name=Modelo")
        {
        }

        public virtual DbSet<T_Areas> T_Areas { get; set; }
        public virtual DbSet<T_Empleados> T_Empleados { get; set; }
        public virtual DbSet<T_Materiales> T_Materiales { get; set; }
        public virtual DbSet<T_Roles> T_Roles { get; set; }
        public virtual DbSet<T_Transacciones> T_Transacciones { get; set; }
        public virtual DbSet<T_Usuarios> T_Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_Areas>()
                .Property(e => e.NombreArea)
                .IsUnicode(false);

            modelBuilder.Entity<T_Areas>()
                .HasMany(e => e.T_Empleados)
                .WithRequired(e => e.T_Areas)
                .HasForeignKey(e => e.AreaID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Empleados>()
                .Property(e => e.NombreCompleto)
                .IsUnicode(false);

            modelBuilder.Entity<T_Empleados>()
                .Property(e => e.Puesto)
                .IsUnicode(false);

            modelBuilder.Entity<T_Empleados>()
                .Property(e => e.Cara)
                .IsUnicode(false);

            modelBuilder.Entity<T_Empleados>()
                .HasMany(e => e.T_Transacciones)
                .WithRequired(e => e.T_Empleados)
                .HasForeignKey(e => e.EmpleadoID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Empleados>()
                .HasMany(e => e.T_Usuarios)
                .WithRequired(e => e.T_Empleados)
                .HasForeignKey(e => e.EmpleadoID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Materiales>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<T_Materiales>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<T_Materiales>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<T_Materiales>()
                .HasMany(e => e.T_Transacciones)
                .WithRequired(e => e.T_Materiales)
                .HasForeignKey(e => e.Material_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Roles>()
                .Property(e => e.NombreRol)
                .IsUnicode(false);

            modelBuilder.Entity<T_Roles>()
                .HasMany(e => e.T_Usuarios)
                .WithRequired(e => e.T_Roles)
                .HasForeignKey(e => e.Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Usuarios>()
                .Property(e => e.Contraseña)
                .IsUnicode(false);

            modelBuilder.Entity<T_Usuarios>()
                .Property(e => e.contraseñaRostro)
                .IsUnicode(false);
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VersaControl.Server.Models.versacontrol;

namespace VersaControl.Server.Data
{
    public partial class versacontrolContext : DbContext
    {
        public versacontrolContext()
        {
        }

        public versacontrolContext(DbContextOptions<versacontrolContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<VersaControl.Server.Models.versacontrol.Aspnetuserlogin>().HasKey(table => new {
                table.LoginProvider, table.ProviderKey
            });

            builder.Entity<VersaControl.Server.Models.versacontrol.Aspnetuserrole>().HasKey(table => new {
                table.UserId, table.RoleId
            });

            builder.Entity<VersaControl.Server.Models.versacontrol.Aspnetusertoken>().HasKey(table => new {
                table.UserId, table.LoginProvider, table.Name
            });

            builder.Entity<VersaControl.Server.Models.versacontrol.Anexa>()
              .HasOne(i => i.Contracte)
              .WithMany(i => i.Anexas)
              .HasForeignKey(i => i.IdContract)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Anexa>()
              .HasOne(i => i.Monede)
              .WithMany(i => i.Anexas)
              .HasForeignKey(i => i.IdMoneda)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Aspnetroleclaim>()
              .HasOne(i => i.Aspnetrole)
              .WithMany(i => i.Aspnetroleclaims)
              .HasForeignKey(i => i.RoleId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Aspnetuserclaim>()
              .HasOne(i => i.Aspnetuser)
              .WithMany(i => i.Aspnetuserclaims)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Aspnetuserlogin>()
              .HasOne(i => i.Aspnetuser)
              .WithMany(i => i.Aspnetuserlogins)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Aspnetuserrole>()
              .HasOne(i => i.Aspnetrole)
              .WithMany(i => i.Aspnetuserroles)
              .HasForeignKey(i => i.RoleId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Aspnetuserrole>()
              .HasOne(i => i.Aspnetuser)
              .WithMany(i => i.Aspnetuserroles)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Aspnetusertoken>()
              .HasOne(i => i.Aspnetuser)
              .WithMany(i => i.Aspnetusertokens)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Beneficiari>()
              .HasOne(i => i.Roluri)
              .WithMany(i => i.Beneficiaris)
              .HasForeignKey(i => i.Rol)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Contracte>()
              .HasOne(i => i.Beneficiari)
              .WithMany(i => i.Contractes)
              .HasForeignKey(i => i.IdBeneficiar)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Contracte>()
              .HasOne(i => i.Contractori)
              .WithMany(i => i.Contractes)
              .HasForeignKey(i => i.IdFurnizor)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Contracte>()
              .HasOne(i => i.Monede)
              .WithMany(i => i.Contractes)
              .HasForeignKey(i => i.IdMoneda)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Contracte>()
              .HasOne(i => i.TipuriContract)
              .WithMany(i => i.Contractes)
              .HasForeignKey(i => i.IdTipContract)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Contractori>()
              .HasOne(i => i.Roluri)
              .WithMany(i => i.Contractoris)
              .HasForeignKey(i => i.Rol)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<VersaControl.Server.Models.versacontrol.Aspnetuser>()
              .Property(p => p.LockoutEnd)
              .HasColumnType("datetime(6)");
            this.OnModelBuilding(builder);
        }

        public DbSet<VersaControl.Server.Models.versacontrol.Efmigrationshistory> Efmigrationshistories { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.AdminSetting> AdminSettings { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Anexa> Anexas { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> Aspnetroleclaims { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Aspnetrole> Aspnetroles { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> Aspnetuserclaims { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> Aspnetuserlogins { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Aspnetuserrole> Aspnetuserroles { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Aspnetuser> Aspnetusers { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Aspnetusertoken> Aspnetusertokens { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Beneficiari> Beneficiaris { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Contracte> Contractes { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Contractori> Contractoris { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Monede> Monedes { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Roluri> Roluris { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.TipuriContract> TipuriContracts { get; set; }
    }
}
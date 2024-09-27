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
            this.OnModelBuilding(builder);
        }

        public DbSet<VersaControl.Server.Models.versacontrol.Beneficiari> Beneficiaris { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Contractori> Contractoris { get; set; }

        public DbSet<VersaControl.Server.Models.versacontrol.Roluri> Roluris { get; set; }
    }
}
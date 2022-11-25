using System;
using CDKPlugin.Entities;
using Microsoft.EntityFrameworkCore;
using OpenMod.EntityFrameworkCore;
using OpenMod.EntityFrameworkCore.Configurator;

namespace CDKPlugin.Database
{
    public class CDKPluginDbContext : OpenModDbContext<CDKPluginDbContext>
    {
        public CDKPluginDbContext(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public CDKPluginDbContext(IDbContextConfigurator configurator, IServiceProvider serviceProvider) : base(configurator, serviceProvider)
        {
        }

        public DbSet<CDKData> CdkData => Set<CDKData>();
        public DbSet<LogData> LogData => Set<LogData>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CDKData>().HasKey(x => x.CKey);

            //需要解释
            modelBuilder.Entity<LogData>().HasIndex(x => new { x.CKey, x.SteamID }).IsUnique();
        }
    }
}
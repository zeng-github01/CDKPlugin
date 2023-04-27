using System;
using CDKPlugin.Entities;
//using CDKPlugin.Entities.CDKData;
using Microsoft.EntityFrameworkCore;
using OpenMod.EntityFrameworkCore;
using OpenMod.EntityFrameworkCore.Configurator;

namespace CDKPlugin.Contexts
{
    public class CDKPluginDbContext : OpenModDbContext<CDKPluginDbContext>
    {
        public CDKPluginDbContext(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public CDKPluginDbContext(IDbContextConfigurator configurator, IServiceProvider serviceProvider) : base(configurator, serviceProvider)
        {
        }

        public DbSet<CDKData> CDKData => Set<CDKData>();
        public DbSet<LogData> LogData => Set<LogData>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CDKData>().HasKey(x => x.CKey);

            //modelBuilder.Entity<LogData>().HasKey(i => i.CDKData);
            modelBuilder.Entity<LogData>().Property(i => i.RedeemedTime).IsRequired();
            modelBuilder.Entity<LogData>().HasIndex(x => new { x.CDKData, x.SteamID }).IsUnique();
            modelBuilder.Entity<CDKData>().HasMany(x => x.LogDatas).WithOne(y => y.CDKData);
        }
    }
}
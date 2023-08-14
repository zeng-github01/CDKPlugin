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
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CDKData>().HasKey(x => x.CKey);
            modelBuilder.Entity<LogData>().HasKey(x => x.LogID);
            modelBuilder.Entity<CDKData>().Property(i=> i.CKey).IsRequired();
            modelBuilder.Entity<LogData>().Property(x=> x.LogID).ValueGeneratedOnAdd();

            modelBuilder.Entity<LogData>().Property(i => i.RedeemedTime).IsRequired();
            modelBuilder.Entity<LogData>().HasIndex(x => new { x.CDKey, x.SteamID }).IsUnique();
            //modelBuilder.Entity<CDKData>().HasMany(x => x.LogDataList).WithOne(y => y.CDKData).HasForeignKey(x => x.CDKey);
            modelBuilder.Entity<LogData>().HasOne(x => x.Navegation).WithMany(y => y.LogDataList).HasForeignKey(x => x.CDKey);

            //modelBuilder.Entity<CDKData>().Property(i => i.CKey).HasMaxLength(64);
            //modelBuilder.Entity<CDKData>().Property(i => i.CKey).HasColumnType("varchar(64)");
        }
    }
}
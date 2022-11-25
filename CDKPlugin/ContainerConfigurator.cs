using Autofac;
using CDKPlugin.Contexts;
using OpenMod.API.Ioc;
using OpenMod.API.Plugins;
using OpenMod.EntityFrameworkCore;
using OpenMod.EntityFrameworkCore.MySql.Extensions;


namespace CDKPlugin
{
    public class ContainerConfigurator : IPluginContainerConfigurator
    {
        public void ConfigureContainer(IPluginServiceConfigurationContext context)
        {
            context.ContainerBuilder.AddMySqlDbContext<CDKPluginDbContext>();
        }
    }
}
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ECMS.Authorization;

namespace ECMS
{
    [DependsOn(
        typeof(ECMSCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ECMSApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ECMSAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ECMSApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}

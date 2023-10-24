using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ECMS.EntityFrameworkCore;
using ECMS.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ECMS.Web.Tests
{
    [DependsOn(
        typeof(ECMSWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ECMSWebTestModule : AbpModule
    {
        public ECMSWebTestModule(ECMSEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ECMSWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ECMSWebMvcModule).Assembly);
        }
    }
}
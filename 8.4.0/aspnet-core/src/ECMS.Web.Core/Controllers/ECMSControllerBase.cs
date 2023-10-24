using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ECMS.Controllers
{
    public abstract class ECMSControllerBase: AbpController
    {
        protected ECMSControllerBase()
        {
            LocalizationSourceName = ECMSConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ECMS.Authorization
{
    public class ECMSAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            context.CreatePermission(PermissionNames.Pages_Courses, L("CoursesPer"));

            context.CreatePermission(PermissionNames.Pages_Classes, L("ClassesPer"));
            context.CreatePermission(PermissionNames.Pages_UserClasses, L("UserClassPer"));
            context.CreatePermission(PermissionNames.Pages_UserClasses_Register, L("UserRegisterClassPer"));
            
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ECMSConsts.LocalizationSourceName);
        }
    }
}

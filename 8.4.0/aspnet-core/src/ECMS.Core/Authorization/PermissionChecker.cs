using Abp.Authorization;
using ECMS.Authorization.Roles;
using ECMS.Authorization.Users;

namespace ECMS.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

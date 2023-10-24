using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ECMS.Authorization.Roles;
using ECMS.Authorization.Users;
using ECMS.MultiTenancy;

namespace ECMS.EntityFrameworkCore
{
    public class ECMSDbContext : AbpZeroDbContext<Tenant, Role, User, ECMSDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ECMSDbContext(DbContextOptions<ECMSDbContext> options)
            : base(options)
        {
        }
    }
}

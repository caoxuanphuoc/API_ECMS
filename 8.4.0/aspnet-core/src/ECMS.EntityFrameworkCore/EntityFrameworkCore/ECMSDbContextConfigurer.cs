using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ECMS.EntityFrameworkCore
{
    public static class ECMSDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ECMSDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ECMSDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}

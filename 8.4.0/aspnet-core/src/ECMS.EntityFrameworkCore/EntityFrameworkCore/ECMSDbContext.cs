using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ECMS.Authorization.Roles;
using ECMS.Authorization.Users;
using ECMS.MultiTenancy;
using ECMS.UserClassN;
using ECMS.Payment;
using ECMS.Classes;
using ECMS.ScheduleManage.Schedules;
using ECMS.Courses;
using ECMS.Classes.Rooms;
using ECMS.Social.Posts;
using ECMS.HomeWorks;
using ECMS.Social.Comments;
using ECMS.ScheduleManage;
using ECMS.Order;

namespace ECMS.EntityFrameworkCore
{
    public class ECMSDbContext : AbpZeroDbContext<Tenant, Role, User, ECMSDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<UserClass> UserClasses { get; set; }
        public DbSet<TuitionFee> TuitionFees { get; set; }
        public DbSet<TrackingClass> TrackingClasses { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SubmitHomework> SubmitHomeWorks { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public ECMSDbContext(DbContextOptions<ECMSDbContext> options)
            : base(options)
        {
        }
    }
}

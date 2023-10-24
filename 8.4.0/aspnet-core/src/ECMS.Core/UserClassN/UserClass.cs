using Abp.Domain.Entities.Auditing;
using ECMS.Authorization.Users;
using ECMS.Classes;
using ECMS.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.UserClassN
{
    [Table("AbpUserClass")]
    public class UserClass : FullAuditedEntity<long>
    {
        public bool IsActive { get; set; }
        public int OffTimes { get; set; }
        public DateTime DateStart { get; set; }
        public TypeRole RoleMember { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Class")]
        public long ClassId { get; set; }
        public Class Class { get; set; }
      //  public ICollection<TuitionFee> TuitionFees { get; set; }
      //  public ICollection<TrackingClass> TrackingClasses { get; set; }
    }
    public enum TypeRole
    {
        Teacher,
        Student
    }
}

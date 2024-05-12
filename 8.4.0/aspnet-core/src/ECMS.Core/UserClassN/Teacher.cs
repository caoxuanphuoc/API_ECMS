using Abp.Domain.Entities.Auditing;
using ECMS.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.UserClassN
{
    [Table("AbpTeacher")]
    public class Teacher : FullAuditedEntity<long>
    {
        public string SchoolName { get; set; }
        public string Certificate { get; set; }
        public long Wage { get; set; }
        public DateTime StartTime { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}

using Abp.Domain.Entities.Auditing;
using ECMS.Classes.UserClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.UserClassN
{
    [Table("AbpTrackingClass")]
    public class TrackingClass : FullAuditedEntity<long>
    {
        public DateTime Date { get; set; }
        public DateTime CheckInTime { get; set; }
        [ForeignKey("UserClass")]
        public long StudentId { get; set; }
        public UserClass UserClass { get; set; }
    }
}

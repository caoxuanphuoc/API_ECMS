using Abp.Domain.Entities.Auditing;
using ECMS.Classes.UserClass;
using ECMS.ScheduleManage.Schedules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECMS.UserClassN
{
    [Table("AbpTrackingClass")]
    public class TrackingClass : FullAuditedEntity<long>
    {
        public DateTime CheckInTime { get; set; }
        [ForeignKey("UserClass")]
        public long StudentId { get; set; }
        public UserClass UserClass { get; set; }
        public long ScheduleId { get; set; }
    }
}

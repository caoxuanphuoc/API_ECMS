using Abp.Domain.Entities.Auditing;
using ECMS.Classes;
using ECMS.Classes.Rooms;
using ECMS.Courses;
using ECMS.ScheduleManage.Schedules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.ScheduleManage
{
    public class ScheduleClass : FullAuditedEntity<long>
    {
        [ForeignKey("Class")]
        public long ClassId { get; set; } 
        public Class Class { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}

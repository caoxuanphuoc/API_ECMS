using Abp.Domain.Entities.Auditing;
using ECMS.Classes;
using ECMS.Classes.Rooms;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECMS.ScheduleManage.Schedules
{
    [Table("AbpSchedule")]
    public class Schedule : FullAuditedEntity<long>
    {
        public DateTime Date { get; set; }
        [ForeignKey("Class")]
        public long ClassId { get; set; }
        public Class Class { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DayOfTheWeek DayOfWeek { get; set; }
        public Shift Shift { get; set; }
    }
}

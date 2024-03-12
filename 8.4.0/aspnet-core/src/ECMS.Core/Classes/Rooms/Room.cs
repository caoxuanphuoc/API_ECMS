using Abp.Domain.Entities.Auditing;
using ECMS.ScheduleManage.Schedules;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECMS.Classes.Rooms
{
    [Table("AbpRoom")]
    public class Room : FullAuditedEntity<int>
    {
        public string RoomName { get; set; }
        public int MaxContainer { get; set;}
        public ICollection<Schedule> Schedules { get; set; }
    }
}

using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Rooms
{
    [Table("AbpRoom")]
    public class Room : FullAuditedEntity<int>
    {
        public string RoomName { get; set; }
        public int MaxContainer { get; set; }
        //public ICollection<Schedule> Schedules { get; set; }
    }
}

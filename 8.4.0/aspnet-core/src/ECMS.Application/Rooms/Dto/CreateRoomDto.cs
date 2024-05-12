using Abp.AutoMapper;
using ECMS.Classes.Rooms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Rooms.Dto
{
    [AutoMapTo(typeof(Room))]
    public class CreateRoomDto
    {
        [Required]
        public string RoomName { get; set; }
        [Required]
        public int MaxContainer { get; set; }

    }
}

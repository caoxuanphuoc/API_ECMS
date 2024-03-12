using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.Classes.Rooms;
using System;
using System.ComponentModel.DataAnnotations;

namespace ECMS.Rooms.Dto
{
    [AutoMapTo(typeof(Room))]
    public class UpdateRoomDto : EntityDto<int>
    {
        [Required]
        public string RoomName { get; set; }
        [Required]
        public int MaxContainer { get; set; }
    }
}

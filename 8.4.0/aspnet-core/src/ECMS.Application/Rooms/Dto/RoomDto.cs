using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.Classes.Rooms;

namespace ECMS.Rooms.Dto
{
    [AutoMapFrom(typeof(Room))]
    public class RoomDto : EntityDto<int>
    {
        public string RoomName { get; set; }
        public int MaxContainer { get; set; }
    }
}

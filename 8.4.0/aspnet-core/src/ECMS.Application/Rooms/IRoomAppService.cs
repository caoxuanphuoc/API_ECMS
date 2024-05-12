using Abp.Application.Services;
using ECMS.Rooms.Dto;

namespace ECMS.Rooms
{
    public  interface IRoomAppService : IAsyncCrudAppService<RoomDto, int, PagedRoomResultRequestDto, CreateRoomDto, UpdateRoomDto>
    {
    }
}

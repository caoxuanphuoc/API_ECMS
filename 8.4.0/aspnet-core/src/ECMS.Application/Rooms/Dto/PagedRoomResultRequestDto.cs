using Abp.Application.Services.Dto;

namespace ECMS.Rooms.Dto
{
    public class PagedRoomResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

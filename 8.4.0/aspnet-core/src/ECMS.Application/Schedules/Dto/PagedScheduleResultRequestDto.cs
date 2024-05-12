using Abp.Application.Services.Dto;

namespace ECMS.Schedules.Dto
{
    public class PagedScheduleResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long ClassId { get; set; }
    }
}

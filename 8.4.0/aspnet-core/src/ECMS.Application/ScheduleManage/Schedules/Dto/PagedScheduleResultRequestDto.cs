using Abp.Application.Services.Dto;

namespace ECMS.ScheduleManage.Schedules.Dto
{
    public class PagedScheduleResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
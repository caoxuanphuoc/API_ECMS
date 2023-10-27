using Abp.Application.Services.Dto;

namespace ECMS.Courses.Dto
{
    public class PagedCourseResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
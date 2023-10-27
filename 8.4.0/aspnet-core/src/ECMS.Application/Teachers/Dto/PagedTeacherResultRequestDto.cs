using Abp.Application.Services.Dto;

namespace ECMS.Teachers.Dto
{
    public class PagedTeacherResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
using Abp.Application.Services.Dto;

namespace ECMS.Classes.Dto
{
    public class PagedClassResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long ClassId { get; set; }
    }
}
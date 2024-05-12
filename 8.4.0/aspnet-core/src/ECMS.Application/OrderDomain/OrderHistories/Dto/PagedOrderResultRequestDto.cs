using Abp.Application.Services.Dto;

namespace ECMS.OrderDomain.OrderHistories.Dto
{
    public class PagedOrderHistoryResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

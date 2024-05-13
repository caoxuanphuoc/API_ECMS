using Abp.Application.Services.Dto;

namespace ECMS.OrderDomain.Order.Dto
{
    public class PagedHistoryOrderResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
    }
}

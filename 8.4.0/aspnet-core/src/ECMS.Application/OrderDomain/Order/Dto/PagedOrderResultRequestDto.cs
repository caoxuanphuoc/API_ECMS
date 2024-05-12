using Abp.Application.Services.Dto;

namespace ECMS.OrderDomain.Order.Dto
{
    public class PagedOrderResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string CodeOrder { get; set; }
    }
}

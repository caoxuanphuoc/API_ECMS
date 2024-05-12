using Abp.Application.Services;
using ECMS.OrderDomain.OrderHistories.Dto;

namespace ECMS.OrderDomain.OrderHistories
{
    public interface IOrderHistoryAppService: IAsyncCrudAppService<OrderHistoryDto, long, PagedOrderHistoryResultRequestDto, CreateOrderHistoryDto, UpdateOrderHistoryDto>
    {
    }
}

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ECMS.OrderDomain.Order.Dto;
using ECMS.OrderDomain.Order.Dto.ValidateOrderDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECMS.OrderDomain.Order
{
    public interface IOrderAppService : IAsyncCrudAppService<OrderDto, long, PagedOrderResultRequestDto, CreateOrderDto, UpdateOrderDto>
    {
        Task<ValidateOrderDto> ValidateOrder(CreateValidateOrderDto input);
        Task<PagedResultDto<HistoryOrderDto>> GetHistoryOrder(PagedHistoryOrderResultRequestDto input);

    }
}

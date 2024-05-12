using Abp.Application.Services;
using ECMS.OrderDomain.Order.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.OrderDomain.Order
{
    public interface IOrderAppService : IAsyncCrudAppService<OrderDto, long, PagedOrderResultRequestDto, CreateOrderDto, UpdateOrderDto>
    {
    }
}

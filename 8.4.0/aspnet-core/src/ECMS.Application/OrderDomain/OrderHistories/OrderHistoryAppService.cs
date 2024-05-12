using Abp.Application.Services;
using ECMS.Order;
using ECMS.OrderDomain.Order.Dto;
using ECMS.OrderDomain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECMS.OrderDomain.OrderHistories.Dto;
using Abp.Domain.Repositories;

namespace ECMS.OrderDomain.OrderHistories
{
    public class OrderHistoryAppService : AsyncCrudAppService<OrderHistory, OrderHistoryDto, long, PagedOrderHistoryResultRequestDto, CreateOrderHistoryDto, UpdateOrderHistoryDto>, IOrderHistoryAppService
    {
        public OrderHistoryAppService(IRepository<OrderHistory, long> repository) : base(repository)
        {
        }
    }
}

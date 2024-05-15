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
using ECMS.Order.Const;
using Abp.UI;

namespace ECMS.OrderDomain.OrderHistories
{
    public class OrderHistoryAppService : AsyncCrudAppService<OrderHistory, OrderHistoryDto, long, PagedOrderHistoryResultRequestDto, CreateOrderHistoryDto, UpdateOrderHistoryDto>, IOrderHistoryAppService
    {
        private readonly IRepository<Orders, long> _orderRepo;
        public OrderHistoryAppService(IRepository<OrderHistory, long> repository, IRepository<Orders, long> orderRepo) : base(repository)
        {
            _orderRepo = orderRepo;
        }
        public override async Task<OrderHistoryDto> CreateAsync(CreateOrderHistoryDto input)
        {
              var order = await _orderRepo.FirstOrDefaultAsync(x => x.OrderCode == input.OrderCode);
              if( input.ResponseCode == 200 )
              {
                  order.Status = StatusOrder.Success;
              }
              else
              {
                  order.Status = StatusOrder.Cancel;
              }

              
              await CurrentUnitOfWork.SaveChangesAsync();
            var check = await Repository.FirstOrDefaultAsync(x => x.TransactionNo == input.TransactionNo);
                if(check != null )
            {
                throw new UserFriendlyException("Đơn hàng đã được xác nhận");
            }
            return await base.CreateAsync(input);

        }
    }
}

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
using ECMS.UserClasses;
using ECMS.UserClasses.Dto;
using ECMS.Authorization.Users;
using ECMS.Classes.UserClass;

namespace ECMS.OrderDomain.OrderHistories
{
    public class OrderHistoryAppService : AsyncCrudAppService<OrderHistory, OrderHistoryDto, long, PagedOrderHistoryResultRequestDto, CreateOrderHistoryDto, UpdateOrderHistoryDto>, IOrderHistoryAppService
    {
        private readonly IRepository<Orders, long> _orderRepo;
        private readonly IUserClassAppService _userClassAppService;
        public OrderHistoryAppService(IRepository<OrderHistory, long> repository, IRepository<Orders, long> orderRepo) : base(repository)
        {
            _orderRepo = orderRepo;
        }
        public override async Task<OrderHistoryDto> CreateAsync(CreateOrderHistoryDto input)
        {
            var order = await _orderRepo.FirstOrDefaultAsync(x => x.OrderCode == input.OrderCode);
            if (input.ResponseCode == 200)
            {
                order.Status = StatusOrder.Success;

                CreateUserClassDto adduser = new CreateUserClassDto
                {
                    UserId = order.UserId,
                    ClassId = order.ClassId,
                    IsActive = true,
                    RoleMember = TypeRole.Student

                };
                _userClassAppService.CreateAsync(adduser);
              }
            else
            {
                order.Status = StatusOrder.Cancel;
            }

            await CurrentUnitOfWork.SaveChangesAsync();
            var check = await Repository.FirstOrDefaultAsync(x => x.TransactionNo == input.TransactionNo);
            if (check != null)
            {
                throw new UserFriendlyException("Đơn hàng đã được xác nhận");
            }
            return await base.CreateAsync(input);

        }
    }
}

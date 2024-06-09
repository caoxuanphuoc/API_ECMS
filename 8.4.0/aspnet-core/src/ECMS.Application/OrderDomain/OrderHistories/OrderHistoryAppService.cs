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
using ECMS.Classes;

namespace ECMS.OrderDomain.OrderHistories
{
    public class OrderHistoryAppService : AsyncCrudAppService<OrderHistory, OrderHistoryDto, long, PagedOrderHistoryResultRequestDto, CreateOrderHistoryDto, UpdateOrderHistoryDto>, IOrderHistoryAppService
    {
        private readonly IRepository<Orders, long> _orderRepo;
        private readonly IRepository<UserClass, long> _studentRope;
        public OrderHistoryAppService(IRepository<OrderHistory, long> repository,
            IRepository<Orders, long> orderRepo,
            IRepository<UserClass, long> studentRope

            ) : base(repository)
        {
            _orderRepo = orderRepo;
            _studentRope = studentRope;

        }
        public override async Task<OrderHistoryDto> CreateAsync(CreateOrderHistoryDto input)
        {
            var order = await _orderRepo.FirstOrDefaultAsync(x => x.OrderCode == input.OrderCode);
            if (input.ResponseCode == 200)
            {
                order.Status = StatusOrder.Success;

                UserClass adduser = new UserClass
                {
                    UserId = order.UserId,
                    ClassId = order.ClassId,
                    RoleMember = TypeRole.Student,
                    IsActive = true,
                    OffTimes  =0 ,
                    DateStart = DateTime.Now,
                };
               await _studentRope.InsertAsync(adduser);
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

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ECMS.Classes.Dto;
using ECMS.Order;
using ECMS.OrderDomain.Order.Dto;
using ECMS.ScheduleManage.Schedules;
using ECMS.Schedules.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.OrderDomain.Order
{
    public class OrderAppService : AsyncCrudAppService<Orders, OrderDto, long, PagedOrderResultRequestDto, CreateOrderDto, UpdateOrderDto>, IOrderAppService
    {
        public OrderAppService(IRepository<Orders, long> repository) : base(repository)
        {
        }
        public override async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var order = ObjectMapper.Map<Orders>(input);
            var query = Repository.GetAll().Count();
            var codeOrder = "AECMS";
            string numberString = query.ToString();
            while (numberString.Length <= 5)
            {
                numberString = "0"+ numberString;
            }
            codeOrder = codeOrder + numberString;
            order.OrderCode = codeOrder;
            var createOrder = await Repository.InsertAndGetIdAsync(order);
            var getCreateOrderId = new EntityDto<long> { Id = createOrder };
            return await GetAsync(getCreateOrderId);
        }
        public override async Task<PagedResultDto<OrderDto>> GetAllAsync(PagedOrderResultRequestDto input)
        {
            var query = Repository.GetAllIncluding(x => x.User, y => y.Class);
            if (!string.IsNullOrWhiteSpace(input.Keyword))
                query = query.Where(x => x.OrderCode.Contains(input.Keyword));
            if (!string.IsNullOrWhiteSpace(input.CodeOrder))
                query = query.Where(x => x.OrderCode == input.CodeOrder);
            query = query.OrderByDescending(x => x.CreationTime);
            var entities = await query
              .PageBy(input)
              .ToListAsync();
            var totalCount = await query.CountAsync();
            var dtos = ObjectMapper.Map<List<OrderDto>>(entities);
            return new PagedResultDto<OrderDto>(totalCount, dtos);
        }
        public override async Task<OrderDto> GetAsync(EntityDto<long> input)
        {
            CheckGetPermission();
            var classRoom = await Repository.GetAllIncluding(x => x.Class, y=> y.User)
                                        .FirstOrDefaultAsync(x => x.Id == input.Id)
                                        ?? throw new EntityNotFoundException("Not found order");
            var classDto = ObjectMapper.Map<OrderDto>(classRoom);
            return classDto;
        }
    }
}

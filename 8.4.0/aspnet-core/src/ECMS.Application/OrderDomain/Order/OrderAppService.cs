using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Castle.MicroKernel.Registration;
using ECMS.Authorization.Users;
using ECMS.Classes;
using ECMS.Classes.Dto;
using ECMS.Order;
using ECMS.Order.Const;
using ECMS.OrderDomain.Order.Dto;
using ECMS.OrderDomain.Order.Dto.ValidateOrderDto;
using ECMS.ScheduleManage.Schedules;
using ECMS.Schedules.Dto;
using ECMS.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECMS.OrderDomain.Order
{
    public class OrderAppService : AsyncCrudAppService<Orders, OrderDto, long, PagedOrderResultRequestDto, CreateOrderDto, UpdateOrderDto>, IOrderAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Class, long> _classRepository;
        private readonly IRepository<OrderHistory, long> _orderHistoryRepository;


        public OrderAppService(
            IRepository<Orders, long> repository,
           IRepository<User, long> userRepository,
           IRepository<Class, long> classRepository,
           IRepository<OrderHistory, long> orderHistoryRepository
            ) : base(repository)
        {
            _userRepository = userRepository;
            _classRepository = classRepository;
            _orderHistoryRepository = orderHistoryRepository;
        }
        public override async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var order = ObjectMapper.Map<Orders>(input);
            var query = Repository.GetAll().Count();
            var codeOrder = "AECMS";
            string numberString = query.ToString();
            while (numberString.Length <= 5)
            {
                numberString = "0" + numberString;
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
            var classRoom = await Repository.GetAllIncluding(x => x.Class, y => y.User)
                                        .FirstOrDefaultAsync(x => x.Id == input.Id)
                                        ?? throw new EntityNotFoundException("Not found order");
            var classDto = ObjectMapper.Map<OrderDto>(classRoom);
            return classDto;
        }

        public async Task<PagedResultDto<HistoryOrderDto>> GetHistoryOrder(PagedHistoryOrderResultRequestDto input)
        {
            var queryOrder = from order in Repository.GetAllIncluding(x => x.User, y=> y.Class)
                             join history in _orderHistoryRepository.GetAll()
                             on order.OrderCode equals history.OrderCode into jointable
                             from p in jointable.DefaultIfEmpty()
                             select new HistoryOrderDto
                             {
                                OrderCode = order.OrderCode,
                                FullName = order.User.FullName,
                                UserName = order.User.UserName, 
                                StatusOrder = order.Status,
                                CreateOrder = order.CreationTime,
                                ResponseCode = p.ResponseCode,
                                Message = p.Message
                             };
            if( !string.IsNullOrEmpty(input.UserName))
            {
                queryOrder = queryOrder.Where( x => x.UserName == input.UserName );
            }
            if (!string.IsNullOrEmpty(input.OrderCode))
            {
                queryOrder = queryOrder.Where(x => x.OrderCode == input.OrderCode);
            }
            var entities = await queryOrder
              .PageBy(input)
              .ToListAsync();
            var totalCount = await queryOrder.CountAsync();
            var dtos = ObjectMapper.Map<List<HistoryOrderDto>>(entities);
            return new PagedResultDto<HistoryOrderDto>(totalCount, dtos);
        }
        //[Authorize]

        public async Task<ValidateOrderDto> ValidateOrder(CreateValidateOrderDto input)
        {
            var queryUser = await _userRepository.FirstOrDefaultAsync(x => x.Id ==input.UserId);
            if(queryUser == null)
            {
                throw new EntityNotFoundException("User không tồn tại");
            }
            var queryClass = await _classRepository.GetAllIncluding(x => x.Course).FirstOrDefaultAsync(x => x.Id == input.ClassId);
            if (queryClass == null)
            {
                throw new EntityNotFoundException("Class không tồn tại"); 
            }
            decimal DiscountFee = 0m;
            ValidateOrderDto response = new ValidateOrderDto
            {
                UserId = queryUser.Id,
                FullName = queryUser.FullName,
                Email = queryUser.EmailAddress,
                ClassId = queryClass.Id,
                ClassName = queryClass.ClassName,
                CourseName = queryClass.Course.CourseName,
                Fee = queryClass.Course.CourseFee,
                Discount = DiscountFee,
                OrderTotal = queryClass.Course.CourseFee - DiscountFee
            };
            return response;
        }

    }
}

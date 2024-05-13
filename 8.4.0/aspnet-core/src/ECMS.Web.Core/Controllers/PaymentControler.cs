using ECMS.Authorization.Users;
using ECMS.Order;
using ECMS.Order.Const;
using ECMS.OrderDomain.Order;
using ECMS.OrderDomain.Order.Dto;
using ECMS.OrderDomain.Order.Dto.ValidateOrderDto;
using ECMS.Payment;
using ECMS.Payment.Dto;
using ECMS.Users.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Controllers
{
        [Route("api/[controller]/[action]")]
    public class PaymentControler: ECMSControllerBase
    {
        private readonly HttpContext _context;
        private readonly IPaymentAppService _paymentService;
        private readonly IOrderAppService _orderAppService;
        public PaymentControler(HttpContext context,
            IPaymentAppService paymentService,
            OrderAppService orderAppService)
        {
            _context = context;
            _paymentService = paymentService;
            _orderAppService = orderAppService;
        }
        [HttpGet]
        public async Task<string> PaymentConfirm(ValidateOrderDto input)
        {
            decimal discount = input.Fee / 10;
            CreateOrderDto dataOrder = new CreateOrderDto
            {
                UserId=  input.UserId,
                ClassId = input.ClassId,
                Cost = input.Fee,
                Status = StatusOrder.Pending.ToString(),
                TotalDiscount = discount,
                TotalCost = input.Fee - discount

            };
            // create Order with status = 0 (Pending)
            var orderData = await _orderAppService.CreateAsync(dataOrder);

            VnPaymentRequestDto dataVnPay = new VnPaymentRequestDto {
            OrderId = orderData.Id,
            FullName = input.FullName,
            Description = $"Thanh Toán hóa đơn {orderData.OrderCode}",
            Amount = orderData.TotalCost,
            CreatedDate = DateTime.Now,

            };
            var url = _paymentService.GetPaymentRequest(_context, dataVnPay);
            return url;

        }
    }
}

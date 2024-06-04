using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.Authorization.Users;
using ECMS.Classes;
using ECMS.Classes.Rooms;
using ECMS.Order;
using ECMS.Order.Const;
using System;
using System.ComponentModel.DataAnnotations;

namespace ECMS.OrderDomain.Order.Dto
{
    [AutoMapFrom(typeof(Orders))]
    public class OrderDto : EntityDto<long>
    {
        public string OrderCode { get; set; }
        public User User { get; set; }
        public Class Class { get; set; }
        public decimal Cost { get; set; }
        public StatusOrder Status { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.Classes.Rooms;
using ECMS.Order;
using ECMS.Order.Const;
using System;
using System.ComponentModel.DataAnnotations;

namespace ECMS.OrderDomain.Order.Dto
{
    [AutoMapTo(typeof(Orders))]
    public class UpdateOrderDto : EntityDto<long>
    {
        [Required]
        public StatusOrder Status { get; set; }
    }
}

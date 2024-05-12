using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.Authorization.Users;
using ECMS.Classes;
using ECMS.Classes.Rooms;
using ECMS.Order;
using ECMS.Order.Const;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECMS.OrderDomain.OrderHistories.Dto
{
    [AutoMapFrom(typeof(OrderHistory))]
    public class OrderHistoryDto : EntityDto<long>
    {
        public string TransactionNo { get; set; }
        public long ResponseCode { get; set; }
        public string Message { get; set; }
        public long OderId { get; set; }
        public Orders Order { get; set; }
    }
}

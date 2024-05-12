using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.Order;
using System.ComponentModel.DataAnnotations;

namespace ECMS.OrderDomain.OrderHistories.Dto
{
    [AutoMapTo(typeof(OrderHistory))]
    public class UpdateOrderHistoryDto : EntityDto<long>
    {
        [Required]
        public string TransactionNo { get; set; }
        [Required]
        public long ResponseCode { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public long OderId { get; set; }
    }
}

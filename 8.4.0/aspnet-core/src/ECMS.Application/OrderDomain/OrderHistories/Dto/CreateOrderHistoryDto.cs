using Abp.AutoMapper;
using ECMS.Order;
using ECMS.Order.Const;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECMS.OrderDomain.OrderHistories.Dto
{
    [AutoMapTo(typeof(OrderHistory))]
    public class CreateOrderHistoryDto
    {
        [Required]
        public string TransactionNo { get; set; }
        [Required]
        public long ResponseCode { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string OrderCode { get; set; }

    }
}

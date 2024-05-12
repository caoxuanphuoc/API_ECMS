using Abp.AutoMapper;
using ECMS.Order;
using ECMS.Order.Const;
using System.ComponentModel.DataAnnotations;

namespace ECMS.OrderDomain.Order.Dto
{
    [AutoMapTo(typeof(Orders))]
    public class CreateOrderDto
    {
        public long UserId { get; set; }

        public long ClassId { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]

        public string Status { get; set; }
        [Required]
        public decimal TotalDiscount { get; set; }
        [Required]
        public decimal TotalCost { get; set; }

    }
}

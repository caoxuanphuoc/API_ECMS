using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.OrderDomain.Order.Dto.ValidateOrderDto
{
    public class ValidateOrderDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public long ClassId { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public decimal Fee { get; set; }
        public decimal Discount { get; set; }
        public decimal OrderTotal { get; set; }
    }
}

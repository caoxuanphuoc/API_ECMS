using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.OrderDomain.Order.Dto.ValidateOrderDto
{
    public class CreateValidateOrderDto
    {
        public long ClassId { get; set; }
        public long UserId { get; set; }
        public string CodeAffilate { get; set; }
    }
}

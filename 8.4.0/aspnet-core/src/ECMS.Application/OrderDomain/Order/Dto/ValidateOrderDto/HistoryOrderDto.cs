using ECMS.Order.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.OrderDomain.Order.Dto.ValidateOrderDto
{
    public class HistoryOrderDto
    {
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public DateTime CreateOrder { get; set; }
        public long ResponseCode { get; set; }
        public string Message { get; set; }
    }
}

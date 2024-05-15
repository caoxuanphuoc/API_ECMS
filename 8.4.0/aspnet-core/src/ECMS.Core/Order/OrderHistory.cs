using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Order
{
    [Table("AbpOrderHistory")]
    public class OrderHistory : FullAuditedEntity<long>
    {
        public string TransactionNo { get; set; }   
        public long ResponseCode { get; set; }
        public string Message { get; set; }
        [ForeignKey("OrderCode")]
        public string OrderCode { get; set; }
        public Orders Order { get; set; }

    }
}

using Abp.Domain.Entities.Auditing;
using ECMS.Classes.UserClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Payment
{
    [Table("AbpTuitionFee")]
    public class TuitionFee : FullAuditedEntity<long>
    {
        public long Fee { get; set; }
        public DateTime DatePayment { get; set; }
        [ForeignKey("UserClass")]
        public long StudentId { get; set; }
        public UserClass UserClass { get; set; }
    }
}

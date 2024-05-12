using Abp.Domain.Entities.Auditing;
using ECMS.Authorization.Users;
using ECMS.Classes;
using ECMS.Order.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Order
{
    [Table("AbpOrder")]
    public class Orders : FullAuditedEntity<long>
    {
        [Required]
        public string OrderCode { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Class")]
        public long ClassId { get; set; }
        public Class Class { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]

        public StatusOrder Status { get; set; }
        [Required]
        public decimal TotalDiscount { get; set; }
        [Required]
        public decimal TotalCost { get; set; }
    }
}

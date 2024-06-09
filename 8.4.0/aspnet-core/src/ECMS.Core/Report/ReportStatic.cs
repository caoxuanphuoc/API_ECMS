using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Report
{
    [Table("AbpReportStatic")]

    public class ReportStatic : FullAuditedEntity<long>
    {
        [Required]
        public string FeatureCode { get; set; } 
        [Required]

        public string Code { get; set; } 

       [Required]
        public long Value { get; set; }
    }
}

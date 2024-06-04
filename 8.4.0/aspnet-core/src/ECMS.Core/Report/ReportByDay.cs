using Abp.Domain.Entities.Auditing;
using ECMS.Classes.UserClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Report
{
    [Table("AbpReportByDay")]
    public class ReportByDay: FullAuditedEntity<long>
    {
        [Required]
        public string FeatureCode { get; set; }
        [Required]

        public string Code { get; set; }

        [Required]
        public long Value { get; set; }
        public DateTime DateReport { get; set; }
    }
}

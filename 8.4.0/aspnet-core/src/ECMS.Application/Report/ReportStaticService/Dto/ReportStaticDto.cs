using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Report.ReportStaticService.Dto
{
    public class ReportStaticDto
    {
        public string FeatureCode { get; set; }
        public List<ReportField> Reports { get; set; }
    }
    public class ReportField
    {
        public string Lable { get; set; }
        public long Value { get; set;  }
    }
}

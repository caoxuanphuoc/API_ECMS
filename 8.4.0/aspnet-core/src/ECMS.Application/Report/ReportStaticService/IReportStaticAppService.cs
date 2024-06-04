using ECMS.Report.ReportStaticService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Report.ReportStaticService
{
    public interface IReportStaticAppService
    {
        Task<ReportStaticDto> GetStaticReport(string featureCode, DateTime start, DateTime end);
        Task<ReportStaticDto>  GetReportByDate(string code, DateTime start, DateTime end);
        Task MockDataReport(string codeFeature, DateTime start, DateTime end);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Background.Recurringjob.ReportByDate
{
    public interface IReportByDayJob
    {
        Task StudentConvertReport(DateTime nowDay);
        Task RevenueReport(DateTime nowDay);
    }
}

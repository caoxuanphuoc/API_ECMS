using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Background.Recurringjob.ReportStaticJob
{
    public interface IReportStaticJob
    {
        Task ReportStatic();
    }
}

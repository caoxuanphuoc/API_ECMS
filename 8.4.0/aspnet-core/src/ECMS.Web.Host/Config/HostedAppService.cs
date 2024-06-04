using Abp.Application.Services;
using Hangfire;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System;
using ECMS.Background.Recurringjob.ReportStaticJob;
using ECMS.Background.Recurringjob.ReportByDate;

namespace ECMS.Web.Host.Config
{
    [RemoteService(false)]
    public class HostedAppService : IHostedService, IApplicationService
    {
        [Obsolete]
        public Task StartAsync(CancellationToken cancellationToken)
        {
            
            RecurringJob.AddOrUpdate<IReportStaticJob>("ReportTotalStatic", (x) => x.ReportStatic(), "*/5  * * * *");

            // job via day
            DateTime nowDay = DateTime.Now.Date;
            RecurringJob.AddOrUpdate<IReportByDayJob>("StudentConvertReport", (x) => x.StudentConvertReport(nowDay), "*/5  * * * *");
            RecurringJob.AddOrUpdate<IReportByDayJob>("OrderReport", (x) => x.RevenueReport(nowDay), "*/5  * * * *");


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
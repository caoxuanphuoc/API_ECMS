using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using ECMS.Order;
using ECMS.Report.Const;
using ECMS.Report.ReportStaticService.Dto;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Report.ReportStaticService
{
    public class ReportStaticAppService : IReportStaticAppService
    {
        private readonly IRepository<ReportStatic, long> _reportStaticRepository;
        private readonly IRepository<ReportByDay, long> _reportByDayRepository;
        public ReportStaticAppService(IRepository<ReportStatic, long> reportStaticRepository,
            IRepository<ReportByDay, long> reportByDayRepository)
        {
            _reportStaticRepository = reportStaticRepository;
            _reportByDayRepository = reportByDayRepository;
        }
        // param ddax xuwr lys start and end owr controller
        public async Task<ReportStaticDto> GetStaticReport(string featureCode, DateTime start, DateTime end)
        {

            var query = _reportStaticRepository.GetAll().Where(x
                    => x.FeatureCode == featureCode
                    && x.LastModificationTime >= start
                    && x.LastModificationTime <= end
            );
            var result = await query.ToListAsync();
            var mapLable = new Dictionary<string, string>();
            mapLable[ConstKeyReport.ReportTotal.Code.TotalCourse] = "Khóa học";
            mapLable[ConstKeyReport.ReportTotal.Code.TotalStudent] = "Học viên";
            mapLable[ConstKeyReport.ReportTotal.Code.TotalClass] = "Lớp học";
            mapLable[ConstKeyReport.ReportTotal.Code.TotalUser] = "Người dùng";

            List<ReportField> listReport = new List<ReportField>();
            foreach (var item in result)
            {
                ReportField rp = new ReportField
                {
                    Lable = mapLable[item.Code],
                    Value = item.Value,
                };
                listReport.Add(rp);
            }

            return new ReportStaticDto
            {
                FeatureCode = featureCode,
                Reports = listReport
            };

        }

        public async Task<ReportStaticDto> GetReportByDate(string code, DateTime start, DateTime end)
        {
            var query = _reportByDayRepository.GetAll().Where(x
                    => x.Code == code
                    && x.CreationTime.Date >= start.Date
                    && x.CreationTime.Date <= end.Date
            );
            var result = await query.ToListAsync();
            result = result.OrderBy(x => x.CreationTime).ToList();
            string featureCode = "";
            List<ReportField> listReport = new List<ReportField>();
            foreach (var item in result)
            {
                featureCode = item.FeatureCode;
                string dateString = item.CreationTime.ToString("dd/MM/yyyy");
                ReportField rp = new ReportField
                {
                    Lable = dateString,
                    Value = item.Value,
                };
                listReport.Add(rp);
            }
            return new ReportStaticDto
            {
                FeatureCode = code,
                Reports = listReport
            };
        }


        public async Task MockDataReport(string codeFeature, DateTime start, DateTime end)
        {
            for (DateTime currDate = start; currDate.Date <= end.Date; currDate = currDate.AddDays(1))
            {
                if (codeFeature == ConstKeyReportByDay.ReportStudentConvert.Feature)
                {
                    string keyUser = ConstKeyReportByDay.ReportStudentConvert.Code.User;
                    string keyStudent = ConstKeyReportByDay.ReportStudentConvert.Code.Student;
                    Random rnd = new Random();
                    long ranuser = rnd.Next(1, 13);
                    ReportByDay OrderU = new ReportByDay
                    {
                        FeatureCode = codeFeature,
                        Code = keyUser,
                        Value = ranuser,
                        DateReport = currDate,
                        CreationTime = currDate
                    };
                    Random rnd2 = new Random();
                    long ranstud = rnd2.Next(1, 8);
                    ReportByDay OrderStu = new ReportByDay
                    {
                        FeatureCode = codeFeature,
                        Code = keyStudent,
                        Value = ranstud,
                        DateReport = currDate,
                        CreationTime = currDate
                    };
                    await _reportByDayRepository.InsertAsync(OrderU);
                    await _reportByDayRepository.InsertAsync(OrderStu);
                }

                if (codeFeature == ConstKeyReportByDay.ReportOrder.Feature)
                {
                    Random rnd2 = new Random();
                    int ranstud = rnd2.Next(0, 3);
                    int ran2 = rnd2.Next(4, 10);

                    List<long> money = new List<long> { 500000, 350000, 400000, 700000 }; 
                    ReportByDay OrderReveneu = new ReportByDay
                    {
                        FeatureCode = ConstKeyReportByDay.ReportOrder.Feature,
                        Code = ConstKeyReportByDay.ReportOrder.Code.Revenue,
                        Value = money[ranstud] *ran2,
                        DateReport = currDate,
                        CreationTime = currDate
                    };
                    await _reportByDayRepository.InsertAsync(OrderReveneu);
                }
            }
        }

    }
}

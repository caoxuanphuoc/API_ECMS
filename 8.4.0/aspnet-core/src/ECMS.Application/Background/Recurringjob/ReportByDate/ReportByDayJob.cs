using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using ECMS.Authorization.Users;
using ECMS.Classes.UserClass;
using ECMS.Order;
using ECMS.Order.Const;
using ECMS.Report;
using ECMS.Report.Const;
using ECMS.Users;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Background.Recurringjob.ReportByDate
{
    public class ReportByDayJob : IReportByDayJob
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserClass, long> _userClasseRepository;
        private readonly IRepository<ReportByDay, long> _reportByDateRepository;
        private readonly IRepository<Orders, long> _orderRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ReportByDayJob(
                IRepository<User, long> userRepository,
                IRepository<UserClass, long> userClasseRepository,
                IRepository<ReportByDay, long> reportByDateRepository,
                IRepository<Orders, long> orderRepository,
                IUnitOfWorkManager unitOfWorkManager

        )
        {
            _userClasseRepository = userClasseRepository;
            _userRepository = userRepository;
            _reportByDateRepository = reportByDateRepository;
            _orderRepository = orderRepository;
            _unitOfWorkManager = unitOfWorkManager ?? throw new ArgumentNullException(nameof(unitOfWorkManager));
        }

        public async Task RevenueReport(DateTime nowDay)
        {
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                string codeFeature = ConstKeyReportByDay.ReportOrder.Feature;
                string key = ConstKeyReportByDay.ReportOrder.Code.Revenue;


                var TotaloderByDate = await _orderRepository.GetAll().Where(x => x.CreationTime.Date == nowDay && x.Status== StatusOrder.Success).SumAsync(x => x.TotalCost);

                ReportByDay Order = new ReportByDay
                {
                    FeatureCode = codeFeature,
                    Code = key,
                    Value = (long)TotaloderByDate,
                    DateReport = DateTime.Now
                };


                var o = await _reportByDateRepository.GetAll().FirstOrDefaultAsync(x => x.Code == key && x.CreationTime.Date == nowDay);

                if (o == null)
                {
                    await _reportByDateRepository.InsertAsync(Order);
                }
                else
                {
                    var rp = _reportByDateRepository.FirstOrDefault(x => x.Code == key && x.CreationTime.Date == nowDay);
                    rp.Value = Order.Value;
                    rp.DateReport = Order.DateReport;
                    await _reportByDateRepository.UpdateAsync(rp);
                }
                unitOfWork.Complete();
            }
        }

        public async Task StudentConvertReport(DateTime nowDay)
        {
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {

                var totalUser = 0;

                using (_unitOfWorkManager.Current.SetTenantId(1))
                {
                    totalUser = await _userRepository.GetAll().Where(x => x.CreationTime.Date == nowDay).CountAsync();
                }
                var totalStudent = await _userClasseRepository.GetAll().Where(x => x.CreationTime.Date == nowDay).CountAsync();

                List<bool> lsCreate = new List<bool>();

                string codeFeature = ConstKeyReportByDay.ReportStudentConvert.Feature;
                string codeUser = ConstKeyReportByDay.ReportStudentConvert.Code.User;
                string codeStudent = ConstKeyReportByDay.ReportStudentConvert.Code.Student;

                var u = await _reportByDateRepository.GetAll().FirstOrDefaultAsync(x => x.Code == codeUser && x.CreationTime.Date == nowDay);
                var s = await _reportByDateRepository.GetAll().FirstOrDefaultAsync(x => x.Code == codeStudent && x.CreationTime.Date == nowDay);


                bool isCreateUser = u == null;
                bool isCreateStudent = u == null;

                lsCreate.Add(isCreateUser);
                lsCreate.Add(isCreateStudent);

                var ListReport = new List<ReportByDay>();
                // user 
                ReportByDay userR = new ReportByDay
                {
                    FeatureCode = codeFeature,
                    Code = codeUser,
                    Value = totalUser,
                    DateReport = DateTime.Now
                };
                ListReport.Add(userR);
                // Student 
                ReportByDay userS = new ReportByDay
                {
                    FeatureCode = codeFeature,
                    Code = codeStudent,
                    Value = totalStudent,
                    DateReport = DateTime.Now
                };
                ListReport.Add(userS);

                for (int i = 0; i < 2; i++)
                {
                    if (lsCreate[i])
                    {
                        await _reportByDateRepository.InsertAsync(ListReport[i]);
                    }
                    else
                    {
                        var rp = _reportByDateRepository.FirstOrDefault(x => x.Code == ListReport[i].Code && x.CreationTime.Date == nowDay);
                        rp.Value = ListReport[i].Value;
                        rp.DateReport = ListReport[i].DateReport;
                        await _reportByDateRepository.UpdateAsync(rp);
                    }
                }

                unitOfWork.Complete();
            }
        }
    }
}

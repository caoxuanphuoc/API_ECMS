using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using ECMS.Authorization.Users;
using ECMS.Classes;
using ECMS.Classes.UserClass;
using ECMS.Courses;
using ECMS.Report;
using ECMS.Report.Const;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using static ECMS.Report.Const.ConstKeyReport.ReportTotal;

namespace ECMS.Background.Recurringjob.ReportStaticJob
{
    public class ReportStaticJob : IReportStaticJob
    {
        // Các bảng cần thiết
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserClass, long> _userClassRepository;
        private readonly IRepository<Course, long> _courseRepository;
        private readonly IRepository<Class, long> _classRepository;
        private readonly IRepository<ReportStatic, long> _reportStaticRepository;

        public ReportStaticJob(IRepository<User, long> userRepository,
            IRepository<UserClass, long> userClassRepository,
            IRepository<Course, long> courseRepository,
            IRepository<Class, long> classRepository,
            IRepository<ReportStatic, long> reportStaticRepository,
            IUnitOfWorkManager unitOfWorkManager
            )
        {
            _userRepository = userRepository;
            _userClassRepository = userClassRepository;
            _courseRepository = courseRepository;
            _classRepository = classRepository;
            _reportStaticRepository = reportStaticRepository;
            _unitOfWorkManager = unitOfWorkManager ?? throw new ArgumentNullException(nameof(unitOfWorkManager));
        }
        /// <summary>
        /// Quantity User, student, course, class
        /// </summary>
        /// <returns></returns>
        /// 
        
        public async Task ReportStatic()
        {
            #region Check create or update report
            bool userCreate = false;
            bool StudentCreate = false;
            bool CourseCreate = false;
            bool ClassCreate = false;

            string userCode = Code.TotalUser;
            string StudentCode = Code.TotalStudent;
            string CourseCode = Code.TotalCourse;
            string ClassCode = Code.TotalClass;
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                // logic của bạn
                var u = await _reportStaticRepository.GetAll().FirstOrDefaultAsync(x => x.Code == userCode);
                var s = await _reportStaticRepository.GetAll().FirstOrDefaultAsync(x => x.Code == StudentCode);
                var co = await _reportStaticRepository.GetAll().FirstOrDefaultAsync(x => x.Code == CourseCode);
                var cl = await _reportStaticRepository.GetAll().FirstOrDefaultAsync(x => x.Code == ClassCode);


                userCreate = u == null;
                StudentCreate = s == null;
                CourseCreate = co == null;
                ClassCreate = cl == null;

                List<bool> lsStatus = new List<bool>();
                lsStatus.Add(userCreate);
                lsStatus.Add(StudentCreate);
                lsStatus.Add(CourseCreate);
                lsStatus.Add(ClassCreate);

                var ListReport = new List<ReportStatic>();
                #region For user
                using (_unitOfWorkManager.Current.SetTenantId(1))
                {
                    var totalUser = await _userRepository.GetAll().CountAsync();
                    ReportStatic user = new ReportStatic
                    {
                        FeatureCode = Feature,
                        Code = userCode,
                        Value = totalUser
                    };
                    ListReport.Add(user);
                }
                #endregion
                #region For student
                var totalStudent = 0;
               
                    totalStudent = await _userClassRepository.GetAll().CountAsync();
                    ReportStatic student = new ReportStatic
                    {
                        FeatureCode = Feature,
                        Code = StudentCode,
                        Value = totalStudent
                    };
                    ListReport.Add(student);
                

                #endregion
                #region For course
                var totalCourse = await _courseRepository.GetAll().CountAsync();
                ReportStatic course = new ReportStatic
                {
                    FeatureCode = Feature,
                    Code = CourseCode,
                    Value = totalCourse
                };
                ListReport.Add(course);

                #endregion
                #region For class
                var totalClass = await _classRepository.GetAll().CountAsync();
                ReportStatic classe = new ReportStatic
                {
                    FeatureCode = Feature,
                    Code = ClassCode,
                    Value = totalClass
                };
                ListReport.Add(classe);

                #endregion

                #region Create or update
                for (int i = 0; i < 4; i++)
                {
                    if (lsStatus[i])
                    {
                        await _reportStaticRepository.InsertAsync(ListReport[i]);
                    }
                    else
                    {
                        var rp = _reportStaticRepository.FirstOrDefault(x => x.Code == ListReport[i].Code);
                        rp.Value = ListReport[i].Value;
                        await _reportStaticRepository.UpdateAsync(rp);
                    }
                }
                #endregion

                unitOfWork.Complete();
            }


            
            #endregion

           
        }
        
    }
}

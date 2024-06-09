using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using ECMS.Authorization.Users;
using ECMS.Checkin.Dto;
using ECMS.Classes;
using ECMS.Classes.Rooms;
using ECMS.Classes.UserClass;
using ECMS.Common.SystemConst;
using ECMS.OrderDomain.Order.Dto;
using ECMS.ScheduleManage.Schedules;
using ECMS.UserClassN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Checkin
{
    public class TrackingAppService : ITrackingAppService
    {
        private readonly IRepository<TrackingClass, long> _trackngRepository;
        private readonly IRepository<UserClass, long> _userClassRepository;
        private readonly IRepository<Schedule, long> _scheduleRepository;
        private readonly IRepository<Class, long> _classRepository;
        private readonly IRepository<User, long> _userRepository;
        public TrackingAppService(
            IRepository<TrackingClass, long> trackngRepository,
            IRepository<UserClass, long> userClassRepository,
             IRepository<Schedule, long> scheduleRepository,
             IRepository<Class, long> classRepository,
             IRepository<User, long> userRepository
            )
        {
            _trackngRepository = trackngRepository;
            _userClassRepository = userClassRepository;
            _scheduleRepository = scheduleRepository;
            _classRepository = classRepository;
            _userRepository = userRepository;
        }
        public async Task<DetailScheduleDto> CreateTracking(string QRHash)
        {
            string userInfo = DecryptString(QRHash);
            string[] strings = userInfo.Split('-');
            long userId = long.Parse(strings[1]);
            Shift shiftInt = GetCurrentSheet();
            var nowne = DateTime.Now.Date;
            var lsClassOn = _scheduleRepository.GetAll().Where(x => x.Date.Date == nowne && x.Shift == shiftInt);

            var listClassInday = await lsClassOn.Select(x => x.ClassId).ToListAsync();

            var query = _userClassRepository.GetAll()
                        .Where(x => x.UserId == userId && listClassInday.Contains(x.ClassId))
                        .Include(x => x.Class)
                        .Include(x => x.User);

            var studentifor =await query.Select(x => new DetailScheduleDto
            {
                FullName = x.User.FullName,
                Email = x.User.EmailAddress ,
                ClassName = x.Class.ClassName ,
                ClassId = x.Class.Id ,
                CheckinTime = DateTime.Now ,
                StudentId = x.Id
            }).AsNoTracking().FirstOrDefaultAsync();
            if(studentifor != null)
            {

                var chooseSchedule =await lsClassOn.Include(x=> x.Room).FirstOrDefaultAsync(x => studentifor.ClassId == x.ClassId);

                var checkIned =  await _trackngRepository.FirstOrDefaultAsync(x => x.ScheduleId == chooseSchedule.Id && x.StudentId == studentifor.StudentId);
                if(checkIned != null)
                    throw new UserFriendlyException(400,$"{studentifor.FullName} đã điểm danh cho lớp {studentifor.ClassName} rồi nha.");
                var roomInfo = await _classRepository.GetAll().Where(x => x.Id == studentifor.ClassId)
                                .Include(x => x.Course)
                                .FirstOrDefaultAsync();

                if (checkIned == null)
                {
                    studentifor.RoomName = chooseSchedule.Room.RoomName;
                    var trackingData = new TrackingClass
                    {
                        CheckInTime = studentifor.CheckinTime,
                        StudentId = studentifor.StudentId,
                        ScheduleId = chooseSchedule.Id,
                    };
                    await _trackngRepository.InsertAsync(trackingData);
                    studentifor.Notification = "Điểm danh thành công";
                    studentifor.CourseName = roomInfo.Course.CourseName;
                }
                else
                {
                    
                    studentifor.CourseName = roomInfo.Course.CourseName;
                    studentifor.Notification = $"{studentifor.FullName} đã điểm danh cho lớp {studentifor.ClassName} rồi nha.";
                }
                return studentifor;

            }
            else
            {
                throw new UserFriendlyException(400,$"Chưa đến giờ học nha");
            }


        }

        protected string DecryptString(string cipherText)
            {
                string key = KeyHash.KeyHashUser;
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(key);
                    aesAlg.IV = new byte[16]; // Khởi tạo IV bằng mảng byte có giá trị 0

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                try
                {

                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }catch(Exception e)
                {
                    throw new UserFriendlyException(400, "Mã QR không hợp lệ!");
                }
                }
            }
        
        protected Shift GetCurrentSheet()
        {
            DateTime now = DateTime.Now;
           //now = now.AddHours(-4);

            // Thời gian bắt đầu của tiết đầu tiên
            DateTime startTime = new DateTime(now.Year, now.Month, now.Day, 7, 0, 0);

            // Tính toán số tiết
            TimeSpan timeElapsed = now - startTime;
            int totalMinutes = (int)timeElapsed.TotalMinutes;

            if (totalMinutes < 0)
            {
                return Shift.Tiet_1_2; // Trước 7:00 thì trả về tiết 1-2
            }

            int shiftIndex = (totalMinutes+30) / 120;
            // Kiểm tra và giới hạn index trong phạm vi enum
            if (shiftIndex >= Enum.GetValues(typeof(Shift)).Length)
            {
                return (Shift)(Enum.GetValues(typeof(Shift)).Length - 1); // Sau giờ học cuối cùng thì trả về tiết cuối cùng
            }

            return shiftIndex >1 ? (Shift)shiftIndex-1 : (Shift)shiftIndex ;

        }

        public async Task<PagedResultDto<InfoTrackingDto>> GetAll(long classId , int skip, int quantity)
        {
            var q = _trackngRepository.GetAll().Include(x=> x.UserClass).Include(x=> x.UserClass.Class).Include(x => x.UserClass.User);
            var query = _trackngRepository.GetAll().Include(x => x.UserClass).Include(x => x.UserClass.Class).Include(x => x.UserClass.User)
                .Select(n => new InfoTrackingDto
                {
                    Id = n.Id,
                    classId = n.UserClass.ClassId,
                    UserId = n.UserClass.UserId,
                    FullName = n.UserClass.User.FullName,
                    ClassName = n.UserClass.Class.ClassName,
                    CheckinTime =n.CheckInTime
                });

            if (classId != null && classId != 0){
                query = query.Where(x => x.classId == classId); 
            }
            var cn = query.Count();
            query = query.Skip(skip).Take(quantity);
            var res = await query.ToListAsync();
            /*   foreach (var item in query)
               {
                   var u = _userRepository.Get(item.UserId);
                   item.FullName = u.FullName;
               }*/
            return new PagedResultDto<InfoTrackingDto>(cn, res); ;
        }

    }


}

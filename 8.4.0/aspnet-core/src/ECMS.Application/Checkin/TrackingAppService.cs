using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using ECMS.Checkin.Dto;
using ECMS.Classes;
using ECMS.Classes.Rooms;
using ECMS.Classes.UserClass;
using ECMS.Common.SystemConst;
using ECMS.ScheduleManage.Schedules;
using ECMS.UserClassN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
        public TrackingAppService(
            IRepository<TrackingClass, long> trackngRepository,
            IRepository<UserClass, long> userClassRepository,
             IRepository<Schedule, long> scheduleRepository,
             IRepository<Class, long> classRepository
            )
        {
            _trackngRepository = trackngRepository;
            _userClassRepository = userClassRepository;
            _scheduleRepository = scheduleRepository;
            _classRepository = classRepository;
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

                var checkIned =  await _trackngRepository.FirstOrDefaultAsync(x => x.ScheduleId == chooseSchedule.Id);

                
                if(checkIned == null)
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
                }
                else
                {
                    var roomInfo = await _classRepository.GetAll().Where(x => x.Id == studentifor.ClassId)
                                .Include(x => x.Course)
                                .FirstOrDefaultAsync();
                    studentifor.CourseName = roomInfo.Course.CourseName;
                    studentifor.Notification = $"{studentifor.FullName} đã điểm danh cho lớp {studentifor.ClassName} rồi nha.";
                }
                return studentifor;

            }
            else
            {
                var res = new DetailScheduleDto
                {
                    Notification = $"Chưa đến giờ học nha"
                };
                return res;
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
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Checkin.Dto
{
    public class DetailScheduleDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public long ClassId { get; set; }
        public string RoomName { get; set; }
        public DateTime CheckinTime { get; set; }
        public long StudentId { get; set; }
        public string Notification { get; set; }
    }
}

using Abp.AutoMapper;
using System;

namespace ECMS.ScheduleManage.Schedules.Dto
{
    [AutoMapTo(typeof(Schedule))]
    public class CreateScheduleDto
    {
        public DateTime Date { get; set; }
        public int RoomId { get; set; }
        public DayOfTheWeek DayOfWeek { get; set; }
        public Shift Shift { get; set; }
    }
}
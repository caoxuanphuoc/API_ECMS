using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.Classes.Rooms;
using System;

namespace ECMS.ScheduleManage.Schedules.Dto
{
    [AutoMapTo(typeof(Schedule))]
    public class UpdateScheduleDto : EntityDto<long>
    {
        public DateTime Date { get; set; }
        public int RoomId { get; set; }
        public DayOfTheWeek DayOfWeek { get; set; }
        public Shift Shift { get; set; }
    }
}
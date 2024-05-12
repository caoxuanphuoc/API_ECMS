using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.Classes.Dto;
using ECMS.Rooms.Dto;
using ECMS.ScheduleManage.Schedules;
using System;

namespace ECMS.Schedules.Dto
{
    [AutoMapFrom(typeof(Schedule))]
    public class ScheduleDto : EntityDto<long>
    {
        public ClassDto Class { get; set; }
        public RoomDto Room { get; set; }
        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; }
        public string Shift { get; set; }
    }
}

using ECMS.Classes.Rooms;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace ECMS.ScheduleManage.Schedules.Dto
{
    [AutoMapFrom(typeof(Schedule))] 
    public class ScheduleDto : EntityDto<long>
    {
        public DateTime Date { get; set; }
        public int RoomId { get; set; }
        public DayOfTheWeek DayOfWeek { get; set; }
        public Shift Shift { get; set; }
    }
}
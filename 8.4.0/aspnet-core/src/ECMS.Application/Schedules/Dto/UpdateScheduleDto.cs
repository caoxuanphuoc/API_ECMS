using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.ScheduleManage.Schedules;
using System;
using System.ComponentModel.DataAnnotations;

namespace ECMS.Schedules.Dto
{
    [AutoMapTo(typeof(Schedule))]
    public class UpdateScheduleDto : EntityDto<long>
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public long ClassId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DayOfTheWeek DayOfWeek { get; set; }
        [Required]
        public Shift Shift { get; set; }
    }
}

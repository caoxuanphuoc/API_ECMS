using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.ScheduleManage.Schedules;
using System;

namespace ECMS.ScheduleManage.ScheduleClasses.Dto
{
    [AutoMapTo(typeof(ScheduleClass))]
    public class UpdateScheduleClassDto : EntityDto<long>
    {
        public long ClassId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace ECMS.ScheduleManage.ScheduleClasses.Dto
{
    [AutoMapFrom(typeof(ScheduleClass))] 
    public class ScheduleClassDto : EntityDto<long>
    {
        public long ClassId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
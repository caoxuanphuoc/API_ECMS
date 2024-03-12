using ECMS.Classes.Rooms;
using ECMS.ScheduleManage.Schedules;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Abp.AutoMapper;
using ECMS.Classes;
using System.Collections.Generic;

namespace ECMS.ScheduleManage.ScheduleClasses.Dto
{
    [AutoMapTo(typeof(ScheduleClass))]
    public class CreateScheduleClassDto
    {
        public long ClassId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomId { get; set; }
        public List<TimeStudyDto> TimeStudies { get; set; }
    }
}
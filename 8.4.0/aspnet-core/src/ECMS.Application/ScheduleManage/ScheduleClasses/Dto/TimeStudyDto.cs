using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECMS.ScheduleManage.Schedules;

namespace ECMS.ScheduleManage.ScheduleClasses.Dto
{
    public class TimeStudyDto
    {
        public DayOfTheWeek DayOfWeek { get; set; }
        public Shift Shift { get; set; }
    }
}

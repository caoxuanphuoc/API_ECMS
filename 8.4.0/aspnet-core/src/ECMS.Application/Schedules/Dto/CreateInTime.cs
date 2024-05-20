using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Schedules.Dto
{
    public class CreateInTime : CreateScheduleDto
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set ;}

    }
}

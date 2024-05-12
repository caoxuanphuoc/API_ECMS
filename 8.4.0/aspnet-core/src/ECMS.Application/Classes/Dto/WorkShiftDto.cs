
using ECMS.ScheduleManage.Schedules;

namespace ECMS.Classes.Dto
{
    // để lưu lại các ca học được nhập từ Class
    public class WorkShiftDto
    {
        public DayOfTheWeek DateOfWeek { get; set; }
        public Shift ShiftTime { get; set; }
    }
}

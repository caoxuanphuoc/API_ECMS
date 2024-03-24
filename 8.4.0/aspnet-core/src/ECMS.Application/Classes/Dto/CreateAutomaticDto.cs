using System;
using System.Collections.Generic;

namespace ECMS.Classes.Dto
{
    public class CreateAutomaticDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long ClassId { get; set; }
        public int RoomId { get; set; }
        public List<WorkShiftDto> ListWorkShifts { get; set; }
    }
}

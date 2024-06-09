using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Checkin.Dto
{
    public class InfoTrackingDto
    {
        public long Id { get; set; }

        public long classId { get; set; }

        public long UserId { get; set; }
        
        public string ClassName { get; set; }
        public string FullName { get; set; }

        public DateTime CheckinTime { get; set; }
    }
}

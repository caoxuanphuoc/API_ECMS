using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Models.Tracking
{
    public class PageModelTracking
    {
        public int PageSise { get; set; } = 10;
        public int SkipSize { get; set; } = 0;
        public long ClassId { get; set; } = 0;
    }
}

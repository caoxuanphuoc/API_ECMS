using ECMS.Checkin.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Checkin
{
    public interface ITrackingAppService
    {
        Task<DetailScheduleDto> CreateTracking(string QRHash);

    }
}

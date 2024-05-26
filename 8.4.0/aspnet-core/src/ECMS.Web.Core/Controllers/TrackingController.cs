using ECMS.Checkin;
using ECMS.Order.Const;
using ECMS.OrderDomain.Order.Dto.ValidateOrderDto;
using ECMS.OrderDomain.Order.Dto;
using ECMS.Payment.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECMS.Models.Tracking;
using Abp.UI;

namespace ECMS.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TrackingController : ECMSControllerBase
    {
        private readonly ITrackingAppService _trackingAppService;
        public TrackingController(
            ITrackingAppService trackingAppService)
        {
            _trackingAppService = trackingAppService;
        }
        [HttpPost]
        public async Task<IActionResult> Checkin([FromBody] ScanQRModel input)
        {
            try
            {
               var res =  await _trackingAppService.CreateTracking(input.QRString);
               return Ok(res);

            }
            catch(Exception e) {
               return Ok(e);

                throw new UserFriendlyException(e.Message);
            }
        }
    }
}

using ECMS.Models.Report;
using ECMS.OrderDomain.Order.Dto.ValidateOrderDto;
using ECMS.Report.ReportStaticService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ReportController : ECMSControllerBase
    {
        private IReportStaticAppService _reportStaticService;
        public ReportController(IReportStaticAppService reportStaticService)
        {
            _reportStaticService = reportStaticService;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllReportStatic([FromQuery] string feature, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            if(end == DateTime.MinValue)
                end = DateTime.Now;
            var res  = await _reportStaticService.GetStaticReport(feature, start, end);
            return Ok(res);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllReportByDate([FromQuery] string code, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            if (end == DateTime.MinValue)
                end = DateTime.Now;
            var res = await _reportStaticService.GetReportByDate(code, start, end);
            return Ok(res);
        }

        [HttpPost]

        public async Task<IActionResult> MockData([FromBody] InputMockData input )
        {
            try
            {

            await _reportStaticService.MockDataReport(input.FeatureCode, input.Start, input.End);
            return Ok(true);
            }catch(Exception e){
            return Ok(e);

            }
        }
    }
}

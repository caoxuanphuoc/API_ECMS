using Abp.Application.Services;
using ECMS.ScheduleManage.ScheduleClasses.Dto;
using ECMS.ScheduleManage.ScheduleClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECMS.ScheduleManage.Schedules.Dto;

namespace ECMS.ScheduleManage.Schedules
{
    public interface IScheduleAppService : IAsyncCrudAppService<ScheduleDto, long, PagedScheduleResultRequestDto, CreateScheduleDto, UpdateScheduleDto>
    {
    }
}

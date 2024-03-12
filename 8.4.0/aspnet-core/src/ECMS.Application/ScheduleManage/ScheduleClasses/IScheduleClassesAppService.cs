using Abp.Application.Services;
using ECMS.Rooms.Dto;
using ECMS.ScheduleManage.ScheduleClasses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.ScheduleManage.ScheduleClasses
{
    public interface IScheduleClassesAppService : IAsyncCrudAppService<ScheduleClassDto, long, PagedScheduleClassResultRequestDto, CreateScheduleClassDto, UpdateScheduleClassDto>
    {

    }
}

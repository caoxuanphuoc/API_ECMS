using Abp.Application.Services;
using ECMS.Classes.Rooms;
using ECMS.Rooms.Dto;
using ECMS.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECMS.ScheduleManage.Schedules.Dto;
using Abp.Domain.Repositories;

namespace ECMS.ScheduleManage.Schedules
{
    public class ScheduleAppService : AsyncCrudAppService<Schedule, ScheduleDto, long, PagedScheduleResultRequestDto, CreateScheduleDto, UpdateScheduleDto>, IScheduleAppService
    {
        public ScheduleAppService(IRepository<Schedule, long> repository) : base(repository)
        {
        }
    }
}

using Abp.Application.Services;
using ECMS.Schedules.Dto;
using System.Threading.Tasks;

namespace ECMS.Schedules
{
    public interface IScheduleAppService : IAsyncCrudAppService<ScheduleDto, long, PagedScheduleResultRequestDto, CreateScheduleDto, UpdateScheduleDto>
    {
        Task<string> HashSchedule(long id);
        Task<bool> CreateList(CreateInTime input);
        Task<bool> DeleteList(DeleteListInTime input);

    }
}

using System.Threading.Tasks;
using Abp.Application.Services;
using ECMS.Sessions.Dto;

namespace ECMS.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

using System.Threading.Tasks;
using Abp.Application.Services;
using ECMS.Authorization.Accounts.Dto;

namespace ECMS.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

using Abp.Application.Services;
using ECMS.MultiTenancy.Dto;

namespace ECMS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}


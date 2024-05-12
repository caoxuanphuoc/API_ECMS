using Abp.Application.Services;
using Abp.Dependency;
using ECMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Customer
{
    public interface ICustomerAppService 
    {
        Task<List<UserDto>> GetAllUser(PagedUserResultRequestDto input);
    }
}

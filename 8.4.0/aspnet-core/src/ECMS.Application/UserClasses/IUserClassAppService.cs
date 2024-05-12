using Abp.Application.Services;
using ECMS.UserClasses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.UserClasses
{
    public interface IUserClassAppService : IAsyncCrudAppService<UserClassDto, long, PagedUserClassResultRequestDto, CreateUserClassDto, UpdateUserClassDto>
    {
    }
}

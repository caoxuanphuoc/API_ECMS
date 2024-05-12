using Abp.Application.Services;
using ECMS.Teachers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Teachers
{
    public interface ITeacherAppService : IAsyncCrudAppService<TeacherDto, long, PagedTeacherResultRequestDto, CreateTeacherDto, UpdateTeacherDto>
    {
    }
}

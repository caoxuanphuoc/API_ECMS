using Abp.Application.Services;
using ECMS.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Courses
{
    public interface ICourseAppService : IAsyncCrudAppService<CourseDto, long, PagedCourseResultRequestDto, CreateCourseDto, UpdateCourseDto>
    {
    }
}

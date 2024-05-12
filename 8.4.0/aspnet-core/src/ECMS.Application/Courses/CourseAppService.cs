using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using ECMS.Authorization;
using ECMS.Classes;
using ECMS.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.Extensions;
using ECMS.Classes.Dto;

namespace ECMS.Courses
{
    [AbpAuthorize(PermissionNames.Pages_Courses)]
    public class CourseAppService : AsyncCrudAppService<Course, CourseDto, long, PagedCourseResultRequestDto, CreateCourseDto, UpdateCourseDto>, ICourseAppService
    {
        private readonly IRepository<Class, long> _classRepository;
        public CourseAppService(IRepository<Course, long> repository, IRepository<Class, long> classRepository) : base(repository)
        {
            _classRepository = classRepository;
        }

        public override async Task<CourseDto> CreateAsync(CreateCourseDto input)
        {
            CheckCreatePermission();

            if (input.CourseCode.Length != 5)
                throw new UserFriendlyException("Độ dài của Code phải bằng 5, bắt đầu bằng 3 chữ cái in hoa và 2 chữ số phía sau");
            else
            {
                string temp = input.CourseCode;
                if (temp[0] <65 || temp[0] >90 || temp[1] < 65 || temp[1] > 90 || temp[2] < 65 || temp[2] > 90)
                    throw new UserFriendlyException("Độ dài của Code phải bằng 5, bắt đầu bằng 3 chữ cái in hoa và 2 chữ số phía sau");
                if (temp[3] < 48 || temp[3] > 57 || temp[4] < 48 || temp[4] > 57)
                    throw new UserFriendlyException("Độ dài của Code phải bằng 5, bắt đầu bằng 3 chữ cái in hoa và 2 chữ số phía sau");
            }

            var Course = ObjectMapper.Map<Course>(input);
            var createCourseId = await Repository.InsertAndGetIdAsync(Course);
            var getCreateCourseId = new EntityDto<long> { Id = createCourseId };
            return await GetAsync(getCreateCourseId);
        }
        public override async Task<CourseDto> UpdateAsync(UpdateCourseDto input)
        {
            CheckUpdatePermission();
            if (input.CourseCode.Length != 5)
                throw new UserFriendlyException("Độ dài của Code phải bằng 5, bắt đầu bằng 3 chữ cái in hoa và 2 chữ số phía sau");
            else
            {
                string temp = input.CourseCode;
                if (temp[0] < 65 || temp[0] > 90 || temp[1] < 65 || temp[1] > 90 || temp[2] < 65 || temp[2] > 90)
                    throw new UserFriendlyException("Độ dài của Code phải bằng 5, bắt đầu bằng 3 chữ cái in hoa và 2 chữ số phía sau");
                if (temp[3] < 48 || temp[3] > 57 || temp[4] < 48 || temp[4] > 57)
                    throw new UserFriendlyException("Độ dài của Code phải bằng 5, bắt đầu bằng 3 chữ cái in hoa và 2 chữ số phía sau");
            }
            var courseRoom = await Repository.GetAsync(input.Id);
            ObjectMapper.Map(input, courseRoom);
            await base.UpdateAsync(input);
            return await GetAsync(new EntityDto<long> { Id = input.Id });
        }
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            CheckDeletePermission();
            if (await _classRepository.CountAsync(x => x.CourseId == input.Id) > 0)
            {
                throw new UserFriendlyException($"Class is using Course with id = {input.Id} ");
            }
            await base.DeleteAsync(input);
        }

        protected override IQueryable<Course> CreateFilteredQuery(PagedCourseResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                    .WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
                            x => x.CourseName.ToLower().Contains(input.Keyword.ToLower()) ||
                            x.Quantity.ToString() == input.Keyword.ToLower());
        }
        protected override IQueryable<Course> ApplySorting(IQueryable<Course> query, PagedCourseResultRequestDto input)
        {
            return query.OrderBy(r => r.CourseName).ThenBy(r => r.CourseFee);
        }
    }
}

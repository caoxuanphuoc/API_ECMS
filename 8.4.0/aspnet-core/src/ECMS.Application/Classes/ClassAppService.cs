using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper.Internal.Mappers;
using ECMS.Authorization;
using ECMS.Classes.Dto;
using ECMS.Courses;
using ECMS.Rooms;
using ECMS.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using ECMS.UserClasses.Dto;
using ECMS.UserClassN;

namespace ECMS.Classes
{
    [AbpAuthorize(PermissionNames.Pages_Classes)]
    public class ClassAppService : AsyncCrudAppService<Class, ClassDto, long, PagedClassResultRequestDto, CreateClassDto, UpdateClassDto>, IClassAppService
    {
        private readonly IRepository<Class, long> _repository;
        private readonly IRepository<Schedule, long> _scheduleRepository;
        private readonly IRepository<Course, long> _courseRepository;
        private readonly IRepository<Room, int> _roomRepository;
        public ClassAppService(
            IRepository<Class, long> repository,
            IRepository<Schedule, long> scheduleRepository,
            IRepository<Course, long> courseRepository,
            IRepository<Room, int> roomRepository
        ) : base(repository)
        {
            _repository = repository;
            _scheduleRepository = scheduleRepository;
            _courseRepository = courseRepository;
            _roomRepository = roomRepository;
        }
        // Create Query
        protected override IQueryable<Class> CreateFilteredQuery(PagedClassResultRequestDto input)
        {
            var query = Repository.GetAllIncluding(x => x.Course);
            query = query.Where(x => x.IsActive);

            if (!input.Keyword.IsNullOrWhiteSpace())
            {
                query = query.Where(x => x.LimitStudent.ToString() == input.Keyword ||
                                        x.Code.ToLower().Contains(input.Keyword.ToLower())
                                        && x.IsActive);
            }
            return query;
        }
        // Sorting by User
        protected override IQueryable<Class> ApplySorting(IQueryable<Class> query, PagedClassResultRequestDto input)
        {
            return query.OrderBy(r => r.StartDate);
        }
        // Check Course exists or not
        protected async Task<Course> CheckCourseIsExists(long courseId)
        {
            var course = await _courseRepository.GetAsync(courseId);
            if ((course != null && course.IsDeleted) || course == null)
            {
                throw new EntityNotFoundException("Not found Course");
            }
            return course;
        }

        // Check Room exists or not 
        protected async Task<Room> CheckRoomIsExists(int roomId)
        {
            var room = await _roomRepository.GetAsync(roomId);
            if (room == null || (room != null & room.IsDeleted))
            {
                throw new EntityNotFoundException("Not found Room");
            }
            return room;
        }

        // Get Class
        public override async Task<ClassDto> GetAsync(EntityDto<long> input)
        {
            CheckGetPermission();
            var classRoom = await Repository.GetAllIncluding(x => x.Course)
                                        .FirstOrDefaultAsync(x => x.Id == input.Id)
                                        ?? throw new EntityNotFoundException("Not found Class");
            var classDto = ObjectMapper.Map<ClassDto>(classRoom);
            return classDto;
        }

        // Create automatic schedule
        // Thuật toán
        /// <summary>
        ///  Với mỗi ngày nằm trong khoảng từ startTime đên EndTime 
        ///      + Kiểm tra xem ngày đó là ngày thứ mấy.
        ///          + Nếu ngày đó trong với lịch học (workShift) đã nhập ở class)
        ///          + thì tạo một bản ghi lưu vào schedule table
        ///          Tiếp tục cho đến khi đến ngày kết thúc.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="classId"></param>
        /// <param name="roomId"></param>
        /// <param name="LsWorkShift"></param>
        /// <returns></returns>
        
        protected string CreateNextCodeClass(string CurCode)
        {

            string CourseName = CurCode.Substring(0, 5);
            string CurNum = CurCode.Substring(6, 3);
            string CurChar = CurCode.Substring(5, 1);

            int num = int.Parse(CurNum);

            string Numcode = "";
            string Charcode = CurChar;
            if (num >= 99 && num <= 999)
            {
                if (num == 999)
                {
                    num = 0;
                    var s = CurChar[0];
                    var k = (int)s;
                    Numcode = "000";
                    if (k < 90)
                    {
                        s++;
                        Charcode = s.ToString();
                    }
                    if (k == 90)
                        throw new UserFriendlyException("Đã đạt đến giới hạn mở lớp");
                }
                else
                {
                    num++;
                    Numcode = num.ToString();
                }
                //thow if k>90
            }
            else if (num >= 9 && num < 99)
            {
                num++;
                Numcode = "0" + num.ToString();
            }
            else if (num < 9)
            {
                num++;
                Numcode = "00" + num.ToString();
            }
            string newCode = CourseName + Charcode + Numcode;
            return newCode;
        }
        public override async Task<ClassDto> CreateAsync(CreateClassDto input)
        {
            CheckCreatePermission();
            var courseIfo = await CheckCourseIsExists(input.CourseId);
            var latestClass = await _repository.GetAll().Where(x => x.CourseId == input.CourseId).ToListAsync();
            var maxCodeclass = "";
            if (latestClass.Count == 0)
            {
                maxCodeclass = courseIfo.CourseCode + "A000";
            }
            else
            {
                maxCodeclass = latestClass.OrderByDescending(x => x.Id).FirstOrDefault().Code;
                maxCodeclass = CreateNextCodeClass(maxCodeclass);
            }
            var Class = ObjectMapper.Map<Class>(input);
            Class.Code = maxCodeclass;
            Class.IsActive = true;
            var createClassId = await Repository.InsertAndGetIdAsync(Class);
            var getCreateClassId = new EntityDto<long> { Id = createClassId };
            courseIfo.Quantity++;
            await _courseRepository.UpdateAsync(courseIfo);
            return await GetAsync(getCreateClassId);
        }

        // Update new Class
        public override async Task<ClassDto> UpdateAsync(UpdateClassDto input)
        {
            CheckUpdatePermission();
            var classRoom = await Repository.GetAsync(input.Id);
            ObjectMapper.Map(input, classRoom);
            await base.UpdateAsync(input);
            return await GetAsync(new EntityDto<long> { Id = input.Id });
        }

        // Delete Class
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            CheckDeletePermission();
            var scheduleCount = await _scheduleRepository.CountAsync(x => x.ClassId == input.Id);
            if (scheduleCount > 0)
            {
                throw new UserFriendlyException($"Class is being used with id = {input.Id}");
            }
            await base.DeleteAsync(input);
        }
    }
}

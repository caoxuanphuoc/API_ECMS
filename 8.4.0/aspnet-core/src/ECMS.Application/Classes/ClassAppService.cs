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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using ECMS.UserClasses.Dto;
using ECMS.UserClassN;
using ECMS.ScheduleManage.Schedules;
using ECMS.Classes.Rooms;
using ECMS.Schedules.Dto;
using Newtonsoft.Json;
using System.Reflection;
using Abp.Linq.Extensions;

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
            _scheduleRepository = scheduleRepository;
            _courseRepository = courseRepository;
            _roomRepository = roomRepository;
            _repository = repository;
        }
        // Create Query
        protected override IQueryable<Class> CreateFilteredQuery(PagedClassResultRequestDto input)
        {
            var query = Repository.GetAllIncluding(x => x.Course);

            if (!input.Keyword.IsNullOrWhiteSpace())
            {
                query = query.Where(x => x.LimitStudent.ToString() == input.Keyword ||
                                        x.Code.ToLower().Contains(input.Keyword.ToLower())
                                        && x.IsActive);
            }
            else
            {
                query = query.Where(x => x.IsActive);
            }
            query = query.OrderByDescending(x => x.CreationTime);
            return query;
        }
        public override async Task<PagedResultDto<ClassDto>> GetAllAsync(PagedClassResultRequestDto input)
        {
            var query = Repository.GetAllIncluding(x => x.Course);
            if(!input.Keyword.IsNullOrWhiteSpace())
                query = query.Where( x=> x.ClassName.Contains(input.Keyword) || x.Code.Contains(input.Keyword));
            query = query.OrderByDescending(x => x.CreationTime);
            var entities = await query
              .PageBy(input)
              .ToListAsync();
            var totalCount = await query.CountAsync();
            var dtos = ObjectMapper.Map<List<ClassDto>>(entities);
            foreach( var entity in dtos )
            {
                var cnt = await _scheduleRepository.CountAsync(x=> x.ClassId == entity.Id);
                entity.NumberSchedule = cnt;
            }
            return new PagedResultDto<ClassDto>(totalCount, dtos);
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
            if (room == null || (room != null && room.IsDeleted))
            {
                throw new EntityNotFoundException("Not found Room");
            }
            return room;
        }
        // check code class
        protected async Task<bool> CodeClassIsExists(string code)
        {
            var cl = await _repository.FirstOrDefaultAsync(x => x.Code == code);
            if ( cl !=null)
            {
                throw new UserFriendlyException("Lỗi cú pháp", "Mã lớp học đã tồn tại");
            }
            return false;
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
        protected async Task<PagedResultDto<ScheduleDto>> CreateScheduleAutomatic(CreateAutomaticDto input)
        {
            DateTime Temp = input.StartTime;
            var listSchedule = _scheduleRepository.GetAll();
            List<ScheduleDto> result = new();
            while (Temp <= input.EndTime)
            {
                DayOfWeek checkDOW = Temp.DayOfWeek;
                foreach( var item in input.ListWorkShifts)
                {
                    if ((int) item.DateOfWeek == (int) checkDOW)
                    {
                        Schedule schedule = new()
                        {
                            Date = Temp,
                            ClassId = input.ClassId,
                            RoomId = input.RoomId,
                            DayOfWeek = item.DateOfWeek,
                            Shift = item.ShiftTime
                        };
                        // nếu tồn tại lớp họp thì bỏ qua không thêm
                        //
                        if ( !listSchedule.Any( x=> x.RoomId == input.RoomId && x.Date.Date == Temp.Date && x.Shift == item.ShiftTime) )
                        {
                            await _scheduleRepository.InsertAsync(schedule);
                            var scheduleDto = ObjectMapper.Map<ScheduleDto>(schedule);
                            result.Add(scheduleDto);
                        }
                    }
                }
                UnitOfWorkManager.Current.SaveChanges();
                Temp = Temp.AddDays(1);

            }
            return new PagedResultDto<ScheduleDto>(result.Count, result); ;
        }

        //Create new Class
        public override async Task<ClassDto> CreateAsync(CreateClassDto input)
        {
            CheckCreatePermission();
            await CheckCourseIsExists(input.CourseId);
            await CheckRoomIsExists(input.RoomId);
            await CodeClassIsExists(input.Code);
            var classRoom = ObjectMapper.Map<Class>(input);
            var createClassId = await Repository.InsertAndGetIdAsync(classRoom);
            var createAutomaticDto = new CreateAutomaticDto
            {
                StartTime = input.StartDate,
                EndTime = input.EndDate,
                ClassId = createClassId,
                RoomId = input.RoomId,
                ListWorkShifts = input.lsWorkSheet,
            };

            await CreateScheduleAutomatic(createAutomaticDto);
            var getCreateClassId = new EntityDto<long> { Id = createClassId };
            return await GetAsync(getCreateClassId);
        }

        // Update new Class
        public override async Task<ClassDto> UpdateAsync(UpdateClassDto input)
        {
            CheckUpdatePermission();
            await CheckCourseIsExists(input.CourseId);
            await CheckRoomIsExists(input.RoomId);
            await CodeClassIsExists(input.Code);
            await CheckCourseIsExists(input.CourseId);
            var classRoom = await Repository.GetAsync(input.Id);
            ObjectMapper.Map(input, classRoom);
            await base.UpdateAsync(input);
            return await GetAsync(new EntityDto<long> { Id = input.Id });
        }

        // Delete Class
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            CheckDeletePermission();
            var scheduleList = await _scheduleRepository.GetAll().Where(x => x.ClassId == input.Id).ToListAsync(); //CountAsync(x => x.ClassId == input.Id);
            /*if (scheduleCount > 0)
            {
                throw new UserFriendlyException($"Class is being used with id = {input.Id}");
            }*/
            foreach ( var schedule in scheduleList ) { 
                await _scheduleRepository.DeleteAsync(schedule);
            }
            await base.DeleteAsync(input);
        }
    }
}

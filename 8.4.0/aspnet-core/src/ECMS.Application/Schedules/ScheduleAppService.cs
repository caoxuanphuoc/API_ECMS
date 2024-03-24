﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using ECMS.Authorization;
using ECMS.Classes;
using ECMS.Classes.Rooms;
using ECMS.ScheduleManage.Schedules;
using ECMS.Schedules.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Schedules
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class ScheduleAppService : AsyncCrudAppService<Schedule, ScheduleDto, long, PagedScheduleResultRequestDto, CreateScheduleDto, UpdateScheduleDto>, IScheduleAppService
    {
        private readonly IRepository<Class, long> _classRepository;
        private readonly IRepository<Room, int> _roomRepository;
        public ScheduleAppService(
            IRepository<Schedule, long> repository,
            IRepository<Class, long> classRepository,
            IRepository<Room, int> roomRepository)
            : base(repository)
        {
            _classRepository = classRepository;
            _roomRepository = roomRepository;
        }

        // Sorting by User
        protected override IQueryable<Schedule> ApplySorting(IQueryable<Schedule> query, PagedScheduleResultRequestDto input)
        {
            return query.OrderBy(x => x.ClassId).ThenBy(r => r.Date).ThenBy(r => r.Shift);
        }

        protected override IQueryable<Schedule> CreateFilteredQuery(PagedScheduleResultRequestDto input)
        {
            var query = Repository.GetAllIncluding(x => x.Class, x => x.Class.Course, x => x.Room);
            if (!input.Keyword.IsNullOrWhiteSpace())
            {
                query = query.Where(x => x.Class.Code.ToLower().Contains(input.Keyword.ToLower()) ||
                                        x.Room.RoomName.ToLower().Contains(input.Keyword.ToLower()));
            }

            if (input.ClassId != 0)
            {
                query = query.Where(x => (input.Keyword.IsNullOrWhiteSpace() ||
                                          (x.Class.Code.ToLower().Contains(input.Keyword.ToLower()) ||
                                           x.Room.RoomName.ToLower().Contains(input.Keyword.ToLower()))) &&
                                           x.ClassId == input.ClassId);
            }
            return query;
        }

        // Check Class and Room is exists or not
        protected async Task<(Class classRoom, Room room)> CheckClassAndRoomIsExists(long classId, int roomId)
        {
            var classRoom = await _classRepository.GetAsync(classId);
            var room = await _roomRepository.GetAsync(roomId);
            if (classRoom != null && classRoom.IsActive && !classRoom.IsDeleted)
            {
                return (classRoom, room);
            }
            throw new EntityNotFoundException("Not found Class");
        }

        // Get Schedule
        public override async Task<ScheduleDto> GetAsync(EntityDto<long> input)
        {
            CheckGetPermission();
            var schedule = await Repository.GetAllIncluding(
                                        x => x.Class, x => x.Class.Course, x => x.Room)
                                        .FirstOrDefaultAsync(x => x.Id == input.Id)
                                        ?? throw new EntityNotFoundException("Not found Schedule");
            var scheduleDto = ObjectMapper.Map<ScheduleDto>(schedule);
            return scheduleDto;
        }

        // Create new Schedule
        public override async Task<ScheduleDto> CreateAsync(CreateScheduleDto input)
        {
            CheckCreatePermission();
            await CheckClassAndRoomIsExists(input.ClassId, input.RoomId);
            var schedule = ObjectMapper.Map<Schedule>(input);
            var createSchedule = await Repository.InsertAndGetIdAsync(schedule);
            var getCreateScheduleId = new EntityDto<long> { Id = createSchedule };
            return await GetAsync(getCreateScheduleId);
        }
        // Update Schedule
        public override async Task<ScheduleDto> UpdateAsync(UpdateScheduleDto input)
        {
            CheckUpdatePermission();
            await CheckClassAndRoomIsExists(input.ClassId, input.RoomId);
            var schedule = await Repository.GetAsync(input.Id);
            ObjectMapper.Map(schedule, input);
            await base.UpdateAsync(input);
            return await GetAsync(new EntityDto<long> { Id = input.Id });
        }

        public async Task<string> HashSchedule(long id)
        {
            var schedule = await Repository.GetAsync(id);
            var concatenatedIds = $"{schedule.Id}-{schedule.ClassId}";

            string hashedString;
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(concatenatedIds));
            hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hashedString;
        }
    }
}

using Abp.Application.Services;
using ECMS.ScheduleManage.Schedules.Dto;
using ECMS.ScheduleManage.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECMS.ScheduleManage.ScheduleClasses.Dto;
using Abp.Domain.Repositories;
using ECMS.Classes.Dto;
using Abp.Domain.Entities;

namespace ECMS.ScheduleManage.ScheduleClasses
{
    public class ScheduleClassesAppService : AsyncCrudAppService<ScheduleClass, ScheduleClassDto, long, PagedScheduleClassResultRequestDto, CreateScheduleClassDto, UpdateScheduleClassDto>, IScheduleClassesAppService
    {
        private readonly IRepository<ScheduleClass, long> _repository;
        private readonly IRepository<Schedule, long> _scheduleRepository;
        public ScheduleClassesAppService(
            IRepository<ScheduleClass, long> repository,
            IRepository<Schedule, long> scheduleRepository
            ) : base(repository)
        {
            _repository = repository;
            _scheduleRepository = scheduleRepository;
        }
        /* ClassId  ScheduleId  StartDate  EndDate  */
        protected bool AllowCreateSchedule(CreateScheduleDto input)
        {
            var querySchedule = _scheduleRepository.GetAll().FirstOrDefault(x => x.RoomId == input.RoomId && x.Shift == input.Shift && x.DayOfWeek == input.DayOfWeek && x.Date == input.Date);
            return querySchedule == null ? true : false;
        }
        /*protected async Task CreateAutomatic(Dto.CreateAutomaticDto input)
        {
            foreach (var workShift in input.ListWorkShifts)
            {
                DateTime temp = workShift.StartDate;

                Schedule schedule = new()
                {
                    RoomId = input.RoomId,
                    DayOfWeek = workShift.DateOfWeek,
                    Shift = workShift.ShiftTime
                };

                while (temp <= workShift.EndDate)
                {
                    if (temp.DayOfWeek.ToString() == workShift.DateOfWeek.ToString())
                    {
                        schedule.Date = temp;

                        var createScheduleId = await _scheduleRepository.InsertAndGetIdAsync(schedule);
                        var workShiftDto = new WorkShiftDto
                        {
                            StartDate = temp,
                            EndDate = workShift.EndDate,
                        };
                        var classTimelineDto = new CreateClassTimelineDto
                        {
                            workShift = workShiftDto,
                            ClassId = input.ClassId,
                            ScheduleId = createScheduleId,
                        };

                        await CreateClassTimelineAsync(classTimelineDto);
                    }
                    temp = temp.AddDays(1);
                }
            }
        }*/
        public override async Task<ScheduleClassDto> CreateAsync(CreateScheduleClassDto input)
        {
            DateTime temp = input.StartDate;

            var data = new Schedule
            {
                Date = DateTime.Now,
                RoomId = input.RoomId,
            };
            while (temp <= input.EndDate)
            {
                foreach( var item in input.TimeStudies)
                {
                    var dateOfWeek = (int) temp.DayOfWeek;
                    if( dateOfWeek == (int) item.DayOfWeek)
                    {
                        data.Date = temp;
                        data.DayOfWeek = item.DayOfWeek;
                        data.Shift = item.Shift;
                        var schedule = new CreateScheduleDto
                        {
                            Date = temp,
                            RoomId = input.RoomId,
                            DayOfWeek = item.DayOfWeek,
                            Shift = item.Shift,
                        };
                            
                            //ObjectMapper.Map<CreateScheduleDto>(data);

                        if ( AllowCreateSchedule(schedule) )
                        {
                            await _scheduleRepository.InsertAsync(data);
                        }
                    }
                }
                temp = temp.AddDays(1);
            }
            //Bug here
            var scheduleClass = ObjectMapper.Map<ScheduleClass>(input);

            var scheduleClassCreated =  await _repository.InsertAsync(scheduleClass);
            CurrentUnitOfWork.SaveChanges();
            return  ObjectMapper.Map<ScheduleClassDto>(scheduleClassCreated);
        }
    }
}

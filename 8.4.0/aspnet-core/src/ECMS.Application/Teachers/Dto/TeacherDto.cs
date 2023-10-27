using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.UserClassN;
using ECMS.Users.Dto;
using System;

namespace ECMS.Teachers.Dto
{
    [AutoMapFrom(typeof(Teacher))]
    public class TeacherDto : EntityDto<long>
    {
        public UserDto User { get; set; }
        public string SchoolName { get; set; }
        public string Certificate { get; set; }
        public long Wage { get; set; }
        public DateTime StartTime { get; set; }
    }
}
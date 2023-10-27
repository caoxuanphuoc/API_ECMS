using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.Classes.Dto;
using ECMS.UserClassN;
using ECMS.Users.Dto;
using System;

namespace ECMS.UserClasses.Dto
{
    [AutoMapFrom(typeof(UserClass))]
    public class UserClassDto : EntityDto<long>
    {
        public UserDto User { get; set; }
        public ClassDto Class { get; set; }
        public int OffTimes { get; set; }
        public DateTime DateStart { get; set; }
        public bool IsActive { get; set; }
        public string RoleMember { get; set; }

    }
}
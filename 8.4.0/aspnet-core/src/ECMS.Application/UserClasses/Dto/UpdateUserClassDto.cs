using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.UserClassN;
using System.ComponentModel.DataAnnotations;
using System;

namespace ECMS.UserClasses.Dto
{
    [AutoMapTo(typeof(UserClass))]
    public class UpdateUserClassDto : EntityDto<long>
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public long ClassId { get; set; }
        [Required]
        public int OffTimes { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public TypeRole RoleMember { get; set; }

    }
}
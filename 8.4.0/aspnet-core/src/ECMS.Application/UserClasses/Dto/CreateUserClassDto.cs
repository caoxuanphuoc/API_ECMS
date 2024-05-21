using Abp.AutoMapper;
using ECMS.UserClassN;
using System.ComponentModel.DataAnnotations;
using System;
using ECMS.Classes.UserClass;

namespace ECMS.UserClasses.Dto
{
    [AutoMapTo(typeof(UserClass))]
    public class CreateUserClassDto
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public long ClassId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public TypeRole RoleMember { get; set; }


    }
}
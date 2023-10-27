using Abp.AutoMapper;
using ECMS.UserClassN;
using System.ComponentModel.DataAnnotations;
using System;

namespace ECMS.Teachers.Dto
{
    [AutoMapTo(typeof(Teacher))]
    public class CreateTeacherDto
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string Certificate { get; set; }
        [Required]
        public long Wage { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
    }
}
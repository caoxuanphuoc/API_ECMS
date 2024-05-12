using Abp.AutoMapper;
using Abp.Localization;
using Abp.UI;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ECMS.Courses.Dto
{
    [AutoMapTo(typeof(Course))]
    public class CreateCourseDto
    {
        [Required]
        public string CourseCode { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public long CourseFee { get; set; }
    }
}
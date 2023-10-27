using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace ECMS.Courses.Dto
{
    [AutoMapTo(typeof(Course))]
    public class UpdateCourseDto : EntityDto<long>
    {
        [Required]
        public string CourseCode { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public long CourseFee { get; set; }
        [Required]
        public long Quantity { get; set; }
    }
}
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace ECMS.Courses.Dto
{
    [AutoMapFrom(typeof(Course))]
    public class CourseDto : EntityDto<long>
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public long CourseFee { get; set; }
        public long Quantity { get; set; }
    }
}
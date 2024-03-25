using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ECMS.Courses;
using System;

namespace ECMS.Classes.Dto
{
    [AutoMapFrom(typeof(Class))]
    public class ClassDto : EntityDto<long>
    {
        public string ClassName { get; set; }
        public string Code { get; set; }
        public Course Course { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long LimitStudent { get; set; }
        public long CurrentStudent { get; set; }
        public int LessionTimes { get; set; }
        public bool IsActive { get; set; }
        public int NumberSchedule { get; set; }
    }
}
using Abp.AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace ECMS.Classes.Dto
{
    [AutoMapTo(typeof(Class))]
    public class CreateClassDto
    {
        [Required]
        public string ClassName { get; set; }

        [Required]
        public string Code { get; set; }
        [Required]
        public long CourseId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public long LimitStudent { get; set; }
        [Required]
        public long CurrentStudent { get; set; }
        [Required]
        public int LessionTimes { get; set; }
        [Required]
        public bool IsActive { get; set; }
        // For schedule service
        // Tách RoomId và list workshift ra để tạo schedule
        [Required]
        public int RoomId { get; set; }
        [Required]
        public List<WorkShiftDto> lsWorkSheet { get; set; }
        public string Image { get; set; }
    }
}
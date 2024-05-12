using Abp.Domain.Entities.Auditing;
using ECMS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Courses
{
    [Table("AbpCourse")]
    public class Course : FullAuditedEntity<long>
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public long CourseFee { get; set; }
        public long? Quantity { get; set; }
        //public ICollection<Class> Classes { get; set; }
    }
}

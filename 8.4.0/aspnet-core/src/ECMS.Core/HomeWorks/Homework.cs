using Abp.Domain.Entities.Auditing;
using ECMS.Social.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.HomeWorks
{
    [Table("AbpHomeworks")]
    public class Homework : FullAuditedEntity<long>
    {
        [ForeignKey("Post")]
        public long PostId { get; set; }
        public Post Post { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

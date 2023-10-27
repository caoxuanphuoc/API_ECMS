using Abp.AutoMapper;
using ECMS.UserClassN;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.UserClasses.Dto
{
        [AutoMapTo(typeof(UserClass))]
    public class CreateRegisterDto
    {
            [Required]
            public long UserId { get; set; }
            [Required]
            public long ClassId { get; set; }


        
    }
}

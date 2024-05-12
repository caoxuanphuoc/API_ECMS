using Abp.Application.Services.Dto;
using System;

namespace ECMS.Users.Dto
{
    //custom PagedResultRequestDto
    public class PagedUserResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool? IsActive { get; set; }
    }
}

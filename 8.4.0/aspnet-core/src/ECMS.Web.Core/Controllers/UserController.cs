using ECMS.Authorization.Accounts;
using ECMS.Authorization.Accounts.Dto;
using ECMS.Authorization.Users;
using ECMS.Customer;
using ECMS.Models.User;
using ECMS.Users;
using ECMS.Users.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : ECMSControllerBase
    {
        private readonly IAccountAppService _accountAppService;
        private readonly IUserAppService _userAppService;
        private readonly ICustomerAppService _customerAppService; 
        public UserController(
            IAccountAppService accountAppService
            , IUserAppService userAppService
            , ICustomerAppService customerService
            )
        {
            _accountAppService = accountAppService;
            _userAppService = userAppService;
            _customerAppService = customerService;
        }
        [HttpPost]
        public async Task<RegisterOutput> Resgiter([FromBody] RegisterInput input)
        {
            // validate
            // get all
            var allUser = await _customerAppService.GetAllUser(new PagedUserResultRequestDto{ Email = input.EmailAddress});
            var cgEmail = allUser.FirstOrDefault(x => x.EmailAddress == input.EmailAddress);

            var cgEmail2 = await _customerAppService.GetAllUser(new PagedUserResultRequestDto { UserName = input.UserName });

            /*var x = await cgEmail2.Count();*/
                if (cgEmail != null)
            {
                throw new Abp.UI.UserFriendlyException("Email đã tồn tại");
            }
            if (cgEmail2.Count() !=0)
            {
                throw new Abp.UI.UserFriendlyException("User name đã tồn tại");
            }

            return  await _accountAppService.Register(input);

        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> GetAllUser(PagedUserResultRequestDto input)
        {
            input.SkipCount = 10;
            var ls = await _customerAppService.GetAllUser(input);
            return Ok(ls);

        }
    }
}

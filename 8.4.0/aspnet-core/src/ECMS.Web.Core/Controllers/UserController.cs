using ECMS.Authorization.Accounts;
using ECMS.Authorization.Accounts.Dto;
using ECMS.Authorization.Users;
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
        public UserController(IAccountAppService accountAppService, IUserAppService userAppService)
        {
            _accountAppService = accountAppService;
            _userAppService = userAppService;
        }
        [HttpPost]
        public async Task<RegisterOutput> Resgiter([FromBody] RegisterInput input)
        {
            // validate
            // get all
            var allUser = await _userAppService.GetAllAsync(new PagedUserResultRequestDto { Keyword = input.EmailAddress });
            var cgEmail = allUser.Items.FirstOrDefault(x => x.EmailAddress == input.EmailAddress);

            var allUser2 = await _userAppService.GetAllAsync(new PagedUserResultRequestDto { Keyword = input.UserName});
            var cgEmail2 = allUser2.Items.FirstOrDefault(x => x.UserName == input.UserName);
            if (cgEmail != null)
            {
                throw new Abp.UI.UserFriendlyException("Email đã tồn tại");
            }
            if (cgEmail2 != null)
            {
                throw new Abp.UI.UserFriendlyException("User name đã tồn tại");
            }

            return  await _accountAppService.Register(input);

        }
    }
}

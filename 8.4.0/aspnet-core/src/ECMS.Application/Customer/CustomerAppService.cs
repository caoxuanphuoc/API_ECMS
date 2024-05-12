using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using ECMS.Authorization.Users;
using ECMS.ScheduleManage.Schedules;
using ECMS.Schedules.Dto;
using ECMS.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMS.Customer
{
    public class CustomerAppservice : ICustomerAppService, ITransientDependency
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IObjectMapper _mapper;
        public CustomerAppservice(IRepository<User, long> userRepository,
            IObjectMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async  Task<List<UserDto>> GetAllUser(PagedUserResultRequestDto input)
        {
            var query1 = _userRepository.GetAll();
            var query = _userRepository.GetAll();
            if(!string.IsNullOrEmpty(input.Keyword))
                query = query.Where(x => x.EmailAddress.Contains(input.Keyword)
                || x.UserName.Contains(input.Keyword));

            if (!string.IsNullOrEmpty(input.Email))
                query = query.Where(x => x.EmailAddress == input.Email);

            if (!string.IsNullOrEmpty(input.UserName))
                query = query.Where(x => x.UserName == input.UserName);
            query = query.PageBy(input);
           var items = query.ToList();  
            var scheduleDto = _mapper.Map<List<UserDto>>(items);
            return scheduleDto;

        }
    }
}

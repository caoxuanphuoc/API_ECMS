using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ECMS.Configuration.Dto;

namespace ECMS.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ECMSAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}

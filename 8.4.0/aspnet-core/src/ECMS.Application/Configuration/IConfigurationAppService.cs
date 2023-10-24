using System.Threading.Tasks;
using ECMS.Configuration.Dto;

namespace ECMS.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

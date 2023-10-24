using System.Threading.Tasks;
using ECMS.Models.TokenAuth;
using ECMS.Web.Controllers;
using Shouldly;
using Xunit;

namespace ECMS.Web.Tests.Controllers
{
    public class HomeController_Tests: ECMSWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
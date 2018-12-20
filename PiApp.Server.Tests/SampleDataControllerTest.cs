using PiApp.Server.Controllers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PiApp.Server.Tests
{
    public class SampleDataControllerTest
    {
        [Fact]
        public async Task ShouldReturnWeatherForecasts()
        {
            var controller = new SampleDataController();
            var result = controller.WeatherForecasts();

            Assert.True(result.Any());
        }
    }
}

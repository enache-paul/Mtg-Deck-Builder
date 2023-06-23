using Xunit;
using mtg.Models;
using mtg.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace mtg.Test
{
    [Collection("LoginTests")]
    public class LoginPageTests
    {
        [Fact]
        public void LoginSuccessful()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<AuthenticationController>>();
            var controller = new AuthenticationController(logger);
            var viewModel = new AuthViewModel
            {
                Email = "test@example.com",
                Password = "password"
            };

            var result = controller.Login(viewModel).GetAwaiter().GetResult();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult>(result);
        }
    }
}
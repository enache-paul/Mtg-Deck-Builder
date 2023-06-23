using Xunit;
using mtg.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace mtg.Test
{
    public class DeckControllerTests
    {
        [Fact]
        public void CollectionReturnsView()
        {
            var loggerMock = new Mock<ILogger<DeckController>>();
            var controller = new DeckController(loggerMock.Object);

            var result = controller.Collection();

            Assert.IsType<ViewResult>(result);
        }
    }
}
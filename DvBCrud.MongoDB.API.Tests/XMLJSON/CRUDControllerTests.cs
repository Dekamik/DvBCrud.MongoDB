using DvBCrud.MongoDB.API.Mocks.Controllers;
using DvBCrud.MongoDB.API.XMLJSON;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace DvBCrud.MongoDB.API.Tests.XMLJSON
{
    public class CRUDControllerTests
    {
        private readonly IAnyRepository repository;
        private readonly ILogger logger;
        private readonly AnyController controller;

        public CRUDControllerTests()
        {
            repository = A.Fake<IAnyRepository>();
            logger = A.Fake<ILogger>();
            controller = new AnyController(repository, logger);
        }


        [Fact]
        public void IsActionAllowed_AllowedActionsNotDefined_ReturnsTrue()
        {
            controller.IsActionAllowed(CRUDAction.Create).Should().BeTrue();
            controller.IsActionAllowed(CRUDAction.Read).Should().BeTrue();
            controller.IsActionAllowed(CRUDAction.Update).Should().BeTrue();
            controller.IsActionAllowed(CRUDAction.Delete).Should().BeTrue();
        }

        [Fact]
        public void Read_AnyId_ReturnsModel()
        {
            // Arrange
            var id = "AnyId";
            var expected = new AnyModel
            {
                Id = id,
                AnyString = "AnyString"
            };
            A.CallTo(() => repository.Find(id)).Returns(expected);

            // Act
            var actual = controller.Read(id);

            // Assert
            actual.Should().Be(expected);
        }
    }
}

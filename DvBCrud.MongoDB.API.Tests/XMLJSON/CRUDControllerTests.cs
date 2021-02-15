using DvBCrud.MongoDB.API.Mocks.Controllers;
using DvBCrud.MongoDB.API.XMLJSON;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System.Collections.Generic;
using Xunit;

namespace DvBCrud.MongoDB.API.Tests.XMLJSON
{
    public class CRUDControllerTests
    {
        private readonly IAnyRepository repository;
        private readonly ILogger logger;
        private readonly ICRUDController<AnyModel> controller;

        public CRUDControllerTests()
        {
            repository = A.Fake<IAnyRepository>();
            logger = A.Fake<ILogger>();
            controller = new AnyController(repository, logger);
        }


        [Fact]
        public void IsActionAllowed_AllowedActionsNotDefined_AllActionsAllowed()
        {
            controller.IsActionAllowed(CRUDAction.Create).Should().BeTrue();
            controller.IsActionAllowed(CRUDAction.Read).Should().BeTrue();
            controller.IsActionAllowed(CRUDAction.Update).Should().BeTrue();
            controller.IsActionAllowed(CRUDAction.Delete).Should().BeTrue();
        }


        [Fact]
        public void IsActionAllowed_AnyReadOnlyController_OnlyReadAllowed()
        {
            var readOnlyController = new AnyReadOnlyController(repository, logger);
            readOnlyController.IsActionAllowed(CRUDAction.Create).Should().BeFalse();
            readOnlyController.IsActionAllowed(CRUDAction.Read).Should().BeTrue();
            readOnlyController.IsActionAllowed(CRUDAction.Update).Should().BeFalse();
            readOnlyController.IsActionAllowed(CRUDAction.Delete).Should().BeFalse();
        }

        [Fact]
        public void Read_AnyId_ReturnsModel()
        {
            // Arrange
            string id = ObjectId.GenerateNewId().ToString();
            var expected = new AnyModel
            {
                Id = id,
                AnyString = "AnyString"
            };
            A.CallTo(() => repository.Find(id)).Returns(expected);

            // Act
            var result = controller.Read(id).Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            var actual = result.Value as AnyModel;
            actual.Should().Be(expected);
        }

        [Fact]
        public void Read_ReadForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyTestController(repository, logger, CRUDAction.Create, CRUDAction.Update, CRUDAction.Delete);

            // Act
            var result = restrictedController.Read("AnyId").Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
        }

        [Fact]
        public void ReadAll_Any_ReturnsAllModels()
        {
            // Arrange
            var expected = new[]
            {
                new AnyModel
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    AnyString = "AnyString"
                },
                new AnyModel
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    AnyString = "AnyString"
                }
            };
            A.CallTo(() => repository.Find()).Returns(expected);

            // Act
            var result = controller.ReadAll().Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(expected);
        }

        [Fact]
        public void ReadAll_ReadForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyTestController(repository, logger, CRUDAction.Create, CRUDAction.Update, CRUDAction.Delete);

            // Act
            var result = restrictedController.ReadAll().Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
        }
    }
}

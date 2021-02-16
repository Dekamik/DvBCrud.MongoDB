using DvBCrud.MongoDB.API.Mocks.Controllers;
using DvBCrud.MongoDB.API.XMLJSON;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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
            string id = "AnyId";

            // Act
            var result = restrictedController.Read(id).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Find(id)).MustNotHaveHappened();
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
            A.CallTo(() => repository.Find()).MustNotHaveHappened();
        }

        [Fact]
        public void Create_AnyModel_ModelCreated()
        {
            // Arrange
            var model = new AnyModel
            {
                Id = ObjectId.GenerateNewId().ToString(),
                AnyString = "AnyString"
            };

            // Act
            var result = controller.Create(model) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => repository.Create(model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Create_CreateForbidden_ReturnsForbidden()
        {
            // Arrange
            var readOnlyController = new AnyReadOnlyController(repository, logger);
            var model = new AnyModel();

            // Act
            var result = readOnlyController.Create(new AnyModel()) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Create(model)).MustNotHaveHappened();
        }

        [Fact]
        public void Update_AnyModel_ModelUpdated()
        {
            // Arrange
            string id = ObjectId.GenerateNewId().ToString();
            var model = new AnyModel
            {
                Id = id,
                AnyString = "AnyString"
            };

            // Act
            var result = controller.Update(id, model) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => repository.Update(id, model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Update_UpdateForbidden_ReturnsForbidden()
        {
            // Arrange
            var readOnlyController = new AnyReadOnlyController(repository, logger);
            string id = ObjectId.GenerateNewId().ToString();
            var model = new AnyModel();

            // Act
            var result = readOnlyController.Update(id, model) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Update(id, model)).MustNotHaveHappened();
        }


        [Fact]
        public void Delete_AnyValidId_ModelDeleted()
        {
            // Arrange
            string id = ObjectId.GenerateNewId().ToString();

            // Act
            var result = controller.Delete(id) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => repository.Remove(id)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Delete_DeleteForbidden_ReturnsForbidden()
        {
            // Arrange
            var readOnlyController = new AnyReadOnlyController(repository, logger);
            string id = ObjectId.GenerateNewId().ToString();

            // Act
            var result = readOnlyController.Delete(id) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Remove(id)).MustNotHaveHappened();
        }
    }
}

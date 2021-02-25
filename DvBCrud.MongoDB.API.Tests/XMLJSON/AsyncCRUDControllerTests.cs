using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.API.Mocks.Controllers.Async;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System.Threading.Tasks;
using Xunit;

namespace DvBCrud.MongoDB.API.Tests.XMLJSON
{
    public class AsyncCRUDControllerTests
    {
        private readonly IAnyRepository repository;
        private readonly ILogger logger;
        private readonly IAnyAsyncController controller;

        public AsyncCRUDControllerTests()
        {
            repository = A.Fake<IAnyRepository>();
            logger = A.Fake<ILogger>();
            controller = new AnyAsyncController(repository, logger);
        }

        [Fact]
        public async Task Read_AnyId_ReturnsModel()
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
            var result = (await controller.Read(id)).Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            var actual = result.Value as AnyModel;
            actual.Should().Be(expected);
        }

        [Fact]
        public async Task Read_ReadForbidden_ReturnsForbidden()
        {
            // TODO: Fix restricted controller
            // Arrange
            var restrictedController = new AnyAsyncTestController(repository, logger, CRUDAction.Create, CRUDAction.Update, CRUDAction.Delete);
            string id = "AnyId";

            // Act
            var result = (await restrictedController.Read(id)).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Find(id)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ReadAll_Any_ReturnsAllModels()
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
            var result = (await controller.ReadAll()).Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(expected);
        }

        [Fact]
        public async Task ReadAll_ReadForbidden_ReturnsForbidden()
        {
            // TODO: Async Test Controller
            // Arrange
            var restrictedController = new AnyAsyncTestController(repository, logger, CRUDAction.Create, CRUDAction.Update, CRUDAction.Delete);

            // Act
            var result = (await restrictedController.ReadAll()).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Find()).MustNotHaveHappened();
        }

        [Fact]
        public async Task Create_AnyModel_ModelCreated()
        {
            // Arrange
            var model = new AnyModel
            {
                Id = ObjectId.GenerateNewId().ToString(),
                AnyString = "AnyString"
            };

            // Act
            var result = await controller.Create(model) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => repository.Create(model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Create_CreateForbidden_ReturnsForbidden()
        {
            // TODO: Create Async ReadOnly Controller
            // Arrange
            var readOnlyController = new AnyAsyncReadOnlyController(repository, logger);
            var model = new AnyModel();

            // Act
            var result = await readOnlyController.Create(model) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Create(model)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Update_AnyModel_ModelUpdated()
        {
            // Arrange
            string id = ObjectId.GenerateNewId().ToString();
            var model = new AnyModel
            {
                Id = id,
                AnyString = "AnyString"
            };

            // Act
            var result = await controller.Update(id, model) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => repository.Update(id, model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_UpdateForbidden_ReturnsForbidden()
        {
            // TODO: ReadOnly
            // Arrange
            var readOnlyController = new AnyAsyncReadOnlyController(repository, logger);
            string id = ObjectId.GenerateNewId().ToString();
            var model = new AnyModel();

            // Act
            var result = await readOnlyController.Update(id, model) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Update(id, model)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Delete_AnyValidId_ModelDeleted()
        {
            // Arrange
            string id = ObjectId.GenerateNewId().ToString();

            // Act
            var result = await controller.Delete(id) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => repository.Remove(id)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task Delete_DeleteForbidden_ReturnsForbidden()
        {
            // TODO: Async Read-Only Controller
            // Arrange
            var readOnlyController = new AnyAsyncReadOnlyController(repository, logger);
            string id = ObjectId.GenerateNewId().ToString();

            // Act
            var result = await readOnlyController.Delete(id) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Remove(id)).MustNotHaveHappened();
        }
    }
}

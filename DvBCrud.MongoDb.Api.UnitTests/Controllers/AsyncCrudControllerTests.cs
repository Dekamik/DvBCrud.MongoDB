using System.Threading.Tasks;
using DvBCrud.MongoDB.API.CrudActions;
using DvBCrud.MongoDB.API.Mocks.Controllers.Async;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Xunit;

namespace DvBCrud.MongoDB.API.UnitTests.Controllers
{
    public class AsyncCrudControllerTests
    {
        private readonly IAnyRepository _repository;
        private readonly AnyAsyncController _controller;

        public AsyncCrudControllerTests()
        {
            _repository = A.Fake<IAnyRepository>();
            _controller = new AnyAsyncController(_repository);
        }

        [Fact]
        public async Task Read_AnyId_ReturnsModel()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            var expected = new AnyModel
            {
                Id = id,
                AnyString = "AnyString"
            };
            A.CallTo(() => _repository.FindAsync(id)).Returns(expected);

            // Act
            var result = (await _controller.Read(id)).Result as OkObjectResult;

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
            var restrictedController = new AnyAsyncTestController(_repository);
            var id = "AnyId";

            // Act
            var result = (await restrictedController.Read(id)).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => _repository.FindAsync(id)).MustNotHaveHappened();
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
            A.CallTo(() => _repository.FindAsync()).Returns(expected);

            // Act
            var result = (await _controller.ReadAll()).Result as OkObjectResult;

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
            var restrictedController = new AnyAsyncTestController(_repository);

            // Act
            var result = (await restrictedController.ReadAll()).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => _repository.FindAsync()).MustNotHaveHappened();
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
            var result = await _controller.Create(model) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => _repository.CreateAsync(model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Create_CreateForbidden_ReturnsForbidden()
        {
            // TODO: Create Async ReadOnly Controller
            // Arrange
            var readOnlyController = new AnyAsyncReadOnlyController(_repository);
            var model = new AnyModel();

            // Act
            var result = await readOnlyController.Create(model) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => _repository.CreateAsync(model)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Update_AnyModel_ModelUpdated()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyModel
            {
                Id = id,
                AnyString = "AnyString"
            };

            // Act
            var result = await _controller.Update(id, model) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => _repository.UpdateAsync(id, model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_UpdateForbidden_ReturnsForbidden()
        {
            // TODO: ReadOnly
            // Arrange
            var readOnlyController = new AnyAsyncReadOnlyController(_repository);
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyModel();

            // Act
            var result = await readOnlyController.Update(id, model) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => _repository.UpdateAsync(id, model)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Delete_AnyValidId_ModelDeleted()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();

            // Act
            var result = await _controller.Delete(id) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => _repository.RemoveAsync(id)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task Delete_DeleteForbidden_ReturnsForbidden()
        {
            // TODO: Async Read-Only Controller
            // Arrange
            var readOnlyController = new AnyAsyncReadOnlyController(_repository);
            var id = ObjectId.GenerateNewId().ToString();

            // Act
            var result = await readOnlyController.Delete(id) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => _repository.RemoveAsync(id)).MustNotHaveHappened();
        }
    }
}

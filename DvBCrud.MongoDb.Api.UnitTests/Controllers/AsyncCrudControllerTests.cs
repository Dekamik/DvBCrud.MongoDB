using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var expected = new AnyDataModel
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
            var actual = result.Value as AnyDataModel;
            actual.Should().Be(expected);
        }

        [Fact]
        public async Task Read_ReadForbidden_ReturnsNotAllowed()
        {
            // Arrange
            var restrictedController = new AnyAsyncTestController(_repository);
            var id = "AnyId";

            // Act
            var result = (await restrictedController.Read(id)).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(405);
            A.CallTo(() => _repository.FindAsync(id)).MustNotHaveHappened();
        }
        
        [Fact]
        public async Task Read_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            A.CallTo(() => _repository.FindAsync((string)null))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) (await _controller.Read(null)).Result;

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task ReadAll_Any_ReturnsAllModels()
        {
            // Arrange
            var expected = new[]
            {
                new AnyDataModel
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    AnyString = "AnyString"
                },
                new AnyDataModel
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
            // Arrange
            var restrictedController = new AnyAsyncTestController(_repository);

            // Act
            var result = (await restrictedController.ReadAll()).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(405);
            A.CallTo(() => _repository.FindAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task Create_AnyModel_ModelCreated()
        {
            // Arrange
            var model = new AnyDataModel
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
        public async Task Create_CreateForbidden_ReturnsNotAllowed()
        {
            // Arrange
            var readOnlyController = new AnyAsyncReadOnlyController(_repository);
            var model = new AnyDataModel();

            // Act
            var result = await readOnlyController.Create(model) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(405);
            A.CallTo(() => _repository.CreateAsync(model)).MustNotHaveHappened();
        }
        
        [Fact]
        public async Task Create_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            var model = new AnyDataModel();
            
            A.CallTo(() => _repository.CreateAsync(model))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) await _controller.Create(model);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Update_AnyModel_ModelUpdated()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyDataModel
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
        public async Task Update_UpdateForbidden_ReturnsNotAllowed()
        {
            // Arrange
            var readOnlyController = new AnyAsyncReadOnlyController(_repository);
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyDataModel();

            // Act
            var result = await readOnlyController.Update(id, model) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(405);
            A.CallTo(() => _repository.UpdateAsync(id, model)).MustNotHaveHappened();
        }
        
        [Fact]
        public async Task Update_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyDataModel();

            A.CallTo(() => _repository.UpdateAsync(id, model))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) await _controller.Update(id, model);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Update_ThrowsKeyNotFoundException_ReturnsNotFound()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyDataModel();

            A.CallTo(() => _repository.UpdateAsync(id, model))
                .Throws<KeyNotFoundException>();

            var result = (NotFoundResult) await _controller.Update(id, model);

            result.StatusCode.Should().Be(404);
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
            // Arrange
            var readOnlyController = new AnyAsyncReadOnlyController(_repository);
            var id = ObjectId.GenerateNewId().ToString();

            // Act
            var result = await readOnlyController.Delete(id) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(405);
            A.CallTo(() => _repository.RemoveAsync(id)).MustNotHaveHappened();
        }
        
        [Fact]
        public async Task Delete_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            var id = ObjectId.GenerateNewId().ToString();

            A.CallTo(() => _repository.RemoveAsync(id))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) await _controller.Delete(id);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Delete_ThrowsKeyNotFoundException_ReturnsNotFound()
        {
            var id = ObjectId.GenerateNewId().ToString();

            A.CallTo(() => _repository.RemoveAsync(id))
                .Throws<KeyNotFoundException>();

            var result = (NotFoundResult) await _controller.Delete(id);

            result.StatusCode.Should().Be(404);
        }
    }
}

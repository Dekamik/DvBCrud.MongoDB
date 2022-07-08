using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DvBCrud.MongoDb.Mocks.Controllers.Async;
using DvBCrud.MongoDb.Mocks.Models;
using DvBCrud.MongoDb.Mocks.Repositories;
using DvBCrud.MongoDb.Mocks.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Xunit;

namespace DvBCrud.MongoDb.Api.UnitTests.Controllers
{
    public class AsyncCrudControllerTests
    {
        private readonly IAnyService _repository;
        private readonly AnyAsyncController _controller;

        public AsyncCrudControllerTests()
        {
            _repository = A.Fake<IAnyService>();
            _controller = new AnyAsyncController(_repository);
        }

        [Fact]
        public async Task Read_AnyId_ReturnsModel()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            var expected = new AnyApiModel
            {
                AnyString = "AnyString"
            };
            A.CallTo(() => _repository.GetAsync(id)).Returns(expected);

            // Act
            var result = (await _controller.Read(id)).Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            var actual = result.Value as AnyApiModel;
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
            A.CallTo(() => _repository.GetAsync(id)).MustNotHaveHappened();
        }
        
        [Fact]
        public async Task Read_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            A.CallTo(() => _repository.GetAsync((string)null))
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
                new AnyApiModel
                {
                    AnyString = "AnyString"
                },
                new AnyApiModel
                {
                    AnyString = "AnyString"
                }
            };
            A.CallTo(() => _repository.GetAllAsync()).Returns(expected);

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
            A.CallTo(() => _repository.GetAllAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task Create_AnyModel_ModelCreated()
        {
            // Arrange
            var model = new AnyApiModel
            {
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
            var model = new AnyApiModel();

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
            var model = new AnyApiModel();
            
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
            var model = new AnyApiModel
            {
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
            var model = new AnyApiModel();

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
            var model = new AnyApiModel();

            A.CallTo(() => _repository.UpdateAsync(id, model))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) await _controller.Update(id, model);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Update_ThrowsKeyNotFoundException_ReturnsNotFound()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyApiModel();

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
            A.CallTo(() => _repository.DeleteAsync(id)).MustHaveHappenedOnceExactly();
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
            A.CallTo(() => _repository.DeleteAsync(id)).MustNotHaveHappened();
        }
        
        [Fact]
        public async Task Delete_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            var id = ObjectId.GenerateNewId().ToString();

            A.CallTo(() => _repository.DeleteAsync(id))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) await _controller.Delete(id);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Delete_ThrowsKeyNotFoundException_ReturnsNotFound()
        {
            var id = ObjectId.GenerateNewId().ToString();

            A.CallTo(() => _repository.DeleteAsync(id))
                .Throws<KeyNotFoundException>();

            var result = (NotFoundResult) await _controller.Delete(id);

            result.StatusCode.Should().Be(404);
        }
    }
}

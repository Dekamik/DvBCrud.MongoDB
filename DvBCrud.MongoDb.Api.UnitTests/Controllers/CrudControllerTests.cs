using System;
using System.Collections.Generic;
using DvBCrud.MongoDb.Api.Controllers;
using DvBCrud.MongoDb.Mocks.Controllers.Sync;
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
    public class CrudControllerTests
    {
        private readonly IAnyService _repository;
        private readonly AnyController _controller;

        public CrudControllerTests()
        {
            _repository = A.Fake<IAnyService>();
            _controller = new AnyController(_repository);
        }

        [Fact]
        public void Read_AnyId_ReturnsModel()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            var expected = new AnyApiModel
            {
                AnyString = "AnyString"
            };
            A.CallTo(() => _repository.Get(id)).Returns(expected);

            // Act
            var result = _controller.Read(id).Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            var actual = result.Value as AnyApiModel;
            actual.Should().Be(expected);
        }

        [Fact]
        public void Read_ReadForbidden_ReturnsNotAllowed()
        {
            // Arrange
            var restrictedController = new AnyTestController(_repository);
            var id = "AnyId";

            // Act
            var result = restrictedController.Read(id).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(405);
            A.CallTo(() => _repository.Get(id)).MustNotHaveHappened();
        }
        
        [Fact]
        public void Read_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            A.CallTo(() => _repository.Get((string)null))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) _controller.Read(null).Result;

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public void ReadAll_Any_ReturnsAllModels()
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
            A.CallTo(() => _repository.GetAll()).Returns(expected);

            // Act
            var result = _controller.ReadAll().Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(expected);
        }

        [Fact]
        public void ReadAll_ReadForbidden_ReturnsNotAllowed()
        {
            // Arrange
            var restrictedController = new AnyTestController(_repository);

            // Act
            var result = restrictedController.ReadAll().Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(405);
            A.CallTo(() => _repository.GetAll()).MustNotHaveHappened();
        }

        [Fact]
        public void Create_AnyModel_ModelCreated()
        {
            // Arrange
            var model = new AnyApiModel
            {
                AnyString = "AnyString"
            };

            // Act
            var result = _controller.Create(model) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => _repository.Create(model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Create_CreateForbidden_ReturnsNotAllowed()
        {
            // Arrange
            var readOnlyController = new AnyReadOnlyController(_repository);
            var model = new AnyApiModel();

            // Act
            var result = readOnlyController.Create(model) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(405);
            A.CallTo(() => _repository.Create(model)).MustNotHaveHappened();
        }

        [Fact]
        public void Create_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            var model = new AnyApiModel();
            
            A.CallTo(() => _repository.Create(model))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) _controller.Create(model);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public void Update_AnyModel_ModelUpdated()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyApiModel
            {
                AnyString = "AnyString"
            };

            // Act
            var result = _controller.Update(id, model) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => _repository.Update(id, model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Update_UpdateForbidden_ReturnsNotAllowed()
        {
            // Arrange
            var readOnlyController = new AnyReadOnlyController(_repository);
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyApiModel();

            // Act
            var result = readOnlyController.Update(id, model) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(405);
            A.CallTo(() => _repository.Update(id, model)).MustNotHaveHappened();
        }

        [Fact]
        public void Update_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyApiModel();

            A.CallTo(() => _repository.Update(id, model))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) _controller.Update(id, model);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public void Update_ThrowsKeyNotFoundException_ReturnsNotFound()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyApiModel();

            A.CallTo(() => _repository.Update(id, model))
                .Throws<KeyNotFoundException>();

            var result = (NotFoundResult) _controller.Update(id, model);

            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public void Delete_AnyValidId_ModelDeleted()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();

            // Act
            var result = _controller.Delete(id) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => _repository.Delete(id)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Delete_DeleteForbidden_ReturnsNotAllowed()
        {
            // Arrange
            var readOnlyController = new AnyReadOnlyController(_repository);
            var id = ObjectId.GenerateNewId().ToString();

            // Act
            var result = readOnlyController.Delete(id) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(405);
            A.CallTo(() => _repository.Delete(id)).MustNotHaveHappened();
        }
        
        [Fact]
        public void Delete_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            var id = ObjectId.GenerateNewId().ToString();

            A.CallTo(() => _repository.Delete(id))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) _controller.Delete(id);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public void Delete_ThrowsKeyNotFoundException_ReturnsNotFound()
        {
            var id = ObjectId.GenerateNewId().ToString();

            A.CallTo(() => _repository.Delete(id))
                .Throws<KeyNotFoundException>();

            var result = (NotFoundResult) _controller.Delete(id);

            result.StatusCode.Should().Be(404);
        }
    }
}

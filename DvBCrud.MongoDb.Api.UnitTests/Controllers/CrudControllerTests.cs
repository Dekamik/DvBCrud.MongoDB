using System;
using System.Collections.Generic;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.API.Mocks.Controllers.Sync;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Xunit;

namespace DvBCrud.MongoDB.API.UnitTests.Controllers
{
    public class CrudControllerTests
    {
        private readonly IAnyRepository _repository;
        private readonly CrudController<AnyDataModel, IAnyRepository> _controller;

        public CrudControllerTests()
        {
            _repository = A.Fake<IAnyRepository>();
            _controller = new AnyController(_repository);
        }

        [Fact]
        public void Read_AnyId_ReturnsModel()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            var expected = new AnyDataModel
            {
                Id = id,
                AnyString = "AnyString"
            };
            A.CallTo(() => _repository.Find(id)).Returns(expected);

            // Act
            var result = _controller.Read(id).Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            var actual = result.Value as AnyDataModel;
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
            A.CallTo(() => _repository.Find(id)).MustNotHaveHappened();
        }
        
        [Fact]
        public void Read_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            A.CallTo(() => _repository.Find((string)null))
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
            A.CallTo(() => _repository.Find()).Returns(expected);

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
            A.CallTo(() => _repository.Find()).MustNotHaveHappened();
        }

        [Fact]
        public void Create_AnyModel_ModelCreated()
        {
            // Arrange
            var model = new AnyDataModel
            {
                Id = ObjectId.GenerateNewId().ToString(),
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
            var model = new AnyDataModel();

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
            var model = new AnyDataModel();
            
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
            var model = new AnyDataModel
            {
                Id = id,
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
            var model = new AnyDataModel();

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
            var model = new AnyDataModel();

            A.CallTo(() => _repository.Update(id, model))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) _controller.Update(id, model);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public void Update_ThrowsKeyNotFoundException_ReturnsNotFound()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyDataModel();

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
            A.CallTo(() => _repository.Remove(id)).MustHaveHappenedOnceExactly();
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
            A.CallTo(() => _repository.Remove(id)).MustNotHaveHappened();
        }
        
        [Fact]
        public void Delete_ThrowsArgumentNullException_ReturnsBadRequest()
        {
            var id = ObjectId.GenerateNewId().ToString();

            A.CallTo(() => _repository.Remove(id))
                .Throws<ArgumentNullException>();

            var result = (BadRequestObjectResult) _controller.Delete(id);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public void Delete_ThrowsKeyNotFoundException_ReturnsNotFound()
        {
            var id = ObjectId.GenerateNewId().ToString();

            A.CallTo(() => _repository.Remove(id))
                .Throws<KeyNotFoundException>();

            var result = (NotFoundResult) _controller.Delete(id);

            result.StatusCode.Should().Be(404);
        }
    }
}

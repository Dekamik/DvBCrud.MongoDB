using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.API.CrudActions;
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
        private readonly CrudController<AnyModel, IAnyRepository> _controller;

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
            var expected = new AnyModel
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
            var actual = result.Value as AnyModel;
            actual.Should().Be(expected);
        }

        [Fact]
        public void Read_ReadForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyTestController(_repository);
            var id = "AnyId";

            // Act
            var result = restrictedController.Read(id).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => _repository.Find(id)).MustNotHaveHappened();
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
            A.CallTo(() => _repository.Find()).Returns(expected);

            // Act
            var result = _controller.ReadAll().Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(expected);
        }

        [Fact]
        public void ReadAll_ReadForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyTestController(_repository);

            // Act
            var result = restrictedController.ReadAll().Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => _repository.Find()).MustNotHaveHappened();
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
            var result = _controller.Create(model) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            A.CallTo(() => _repository.Create(model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Create_CreateForbidden_ReturnsForbidden()
        {
            // Arrange
            var readOnlyController = new AnyReadOnlyController(_repository);
            var model = new AnyModel();

            // Act
            var result = readOnlyController.Create(model) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => _repository.Create(model)).MustNotHaveHappened();
        }

        [Fact]
        public void Update_AnyModel_ModelUpdated()
        {
            // Arrange
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyModel
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
        public void Update_UpdateForbidden_ReturnsForbidden()
        {
            // Arrange
            var readOnlyController = new AnyReadOnlyController(_repository);
            var id = ObjectId.GenerateNewId().ToString();
            var model = new AnyModel();

            // Act
            var result = readOnlyController.Update(id, model) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => _repository.Update(id, model)).MustNotHaveHappened();
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
        public void Delete_DeleteForbidden_ReturnsForbidden()
        {
            // Arrange
            var readOnlyController = new AnyReadOnlyController(_repository);
            var id = ObjectId.GenerateNewId().ToString();

            // Act
            var result = readOnlyController.Delete(id) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => _repository.Remove(id)).MustNotHaveHappened();
        }
    }
}

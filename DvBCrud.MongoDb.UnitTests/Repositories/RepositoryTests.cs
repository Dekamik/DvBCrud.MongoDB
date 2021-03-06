﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using DvBCrud.MongoDB.Repositories;
using DvBCrud.MongoDB.Settings;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Xunit;

namespace DvBCrud.MongoDB.Tests.Repositories
{
    public class RepositoryTests
    {
        private readonly IMongoCollectionProxy<AnyModel> _collection;
        private readonly AnyRepository _repository;

        public RepositoryTests()
        {
            var mongoSettings = new MongoSettings
            {
                DatabaseName = "AnyDb",
                CollectionName = "AnyCollection"
            };
            var database = A.Fake<IMongoDatabase>();
            var client = A.Fake<IMongoClient>();
            var logger = A.Fake<ILogger>();
            var options = A.Fake<IOptions<MongoSettings>>();
            _collection = A.Fake<IMongoCollectionProxy<AnyModel>>();
            
            A.CallTo(() => database.GetCollection<AnyModel>(mongoSettings.CollectionName, null)).Returns(_collection);
            A.CallTo(() => client.GetDatabase(mongoSettings.DatabaseName, null)).Returns(database);
            A.CallTo(() => options.Value).Returns(mongoSettings);

            _repository = new AnyRepository(client, logger, options);
        }

        [Fact]
        public void Find_NoArgument_FindCalled()
        {
            _repository.Find();

            A.CallTo(() => _collection.Find(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Find_WithId_FindCalled()
        {
            string id = "AnyId";

            _repository.Find(id);

            A.CallTo(() => _collection.Find(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Find_MissingId_ThrowsArgumentNullException()
        {
            _repository.Invoking(r => r.Find(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task FindAsync_NoArgument_FindAsyncCalled()
        {
            await _repository.FindAsync();

            A.CallTo(() => _collection.FindAsync(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task FindAsync_WithId_FindAsyncCalled()
        {
            string id = "AnyId";

            await _repository.FindAsync(id);

            A.CallTo(() => _collection.FindAsync(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void FindAsync_MissingId_ThrowsArgumentNullException()
        {
            _repository.Awaiting(r => r.FindAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Create_Any_InsertOneCalled()
        {
            // Arrange
            var model = new AnyModel
            {
                AnyString = "AnyString"
            };

            // Act
            _repository.Create(model);

            // Assert
            A.CallTo(() => _collection.InsertOne(model, null, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Create_Multiple_InsertManyCalled()
        {
            // Arrange
            var models = new[]
            {
                new AnyModel
                {
                    AnyString = "AnyString"
                }
            };

            // Act
            _repository.Create(models);

            // Assert
            A.CallTo(() => _collection.InsertMany(models, null, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void CreateAsync_Any_InsertOneCalled()
        {
            // Arrange
            var model = new AnyModel
            {
                AnyString = "AnyString"
            };

            // Act
            _repository.CreateAsync(model);

            // Assert
            A.CallTo(() => _collection.InsertOneAsync(model, null, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void CreateAsync_Multiple_InsertManyCalled()
        {
            // Arrange
            var models = new[]
            {
                new AnyModel
                {
                    AnyString = "AnyString"
                }
            };

            // Act
            _repository.CreateAsync(models);

            // Assert
            A.CallTo(() => _collection.InsertManyAsync(models, null, default)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Update_Any_ReplaceOneCalled()
        {
            string id = "AnyId";
            var model = new AnyModel
            {
                AnyString = "AnyString"
            };

            _repository.Update(id, model);

            A.CallTo(() => _collection.ReplaceOne(A<Expression<Func<AnyModel, bool>>>.Ignored, model)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Update_MissingId_ThrowsArgumentNullException()
        {
            _repository.Invoking(r => r.Update(null, new AnyModel())).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdateAsync_Any_ReplaceOneAsyncCalled()
        {
            string id = "AnyId";
            var model = new AnyModel
            {
                AnyString = "AnyString"
            };

            _repository.UpdateAsync(id, model);

            A.CallTo(() => _collection.ReplaceOneAsync(A<Expression<Func<AnyModel, bool>>>.Ignored, model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void UpdateAsync_MissingId_ThrowsArgumentNullException()
        {
            _repository.Awaiting(r => r.UpdateAsync(null, new AnyModel())).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Remove_AnyId_DeleteOneCalled()
        {
            string id = "AnyId";

            _repository.Remove(id);

            A.CallTo(() => _collection.DeleteOne(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Remove_MissingId_ThrowsArgumentNullException()
        {
            _repository.Invoking(r => r.Remove(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RemoveAsync_AnyId_DeleteOneAsyncCalled()
        {
            string id = "AnyId";

            _repository.RemoveAsync(id);

            A.CallTo(() => _collection.DeleteOneAsync(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void RemoveAsync_MissingId_ThrowsArgumentNullException()
        {
            _repository.Invoking(r => r.RemoveAsync(null)).Should().Throw<ArgumentNullException>();
        }
    }
}

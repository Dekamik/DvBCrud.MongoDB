using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using DvBCrud.MongoDB.Repositories;
using DvBCrud.MongoDB.Settings;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace DvBCrud.MongoDB.Tests.Repositories
{
    public class RepositoryTests
    {
        private readonly IMongoCollectionProxy<AnyModel> collection;
        private readonly IMongoDatabase database;
        private readonly IMongoClient client;
        private readonly ILogger logger;
        private readonly IOptions<MongoSettings> options;
        private readonly AnyRepository repository;

        public RepositoryTests()
        {
            var mongoSettings = new MongoSettings
            {
                DatabaseName = "AnyDb",
                CollectionName = "AnyCollection"
            };
            collection = A.Fake<IMongoCollectionProxy<AnyModel>>();
            database = A.Fake<IMongoDatabase>();
            client = A.Fake<IMongoClient>();
            logger = A.Fake<ILogger>();
            options = A.Fake<IOptions<MongoSettings>>();

            A.CallTo(() => database.GetCollection<AnyModel>(mongoSettings.CollectionName, null)).Returns(collection);
            A.CallTo(() => client.GetDatabase(mongoSettings.DatabaseName, null)).Returns(database);
            A.CallTo(() => options.Value).Returns(mongoSettings);

            repository = new AnyRepository(client, logger, options);
        }

        [Fact]
        public void Find_NoArgument_FindCalled()
        {
            repository.Find();

            A.CallTo(() => collection.Find(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Find_WithId_FindCalled()
        {
            string id = "AnyId";

            repository.Find(id);

            A.CallTo(() => collection.Find(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Find_MissingId_ThrowsArgumentNullException()
        {
            repository.Invoking(r => r.Find(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task FindAsync_NoArgument_FindAsyncCalled()
        {
            await repository.FindAsync();

            A.CallTo(() => collection.FindAsync(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task FindAsync_WithId_FindAsyncCalled()
        {
            string id = "AnyId";

            await repository.FindAsync(id);

            A.CallTo(() => collection.FindAsync(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void FindAsync_MissingId_ThrowsArgumentNullException()
        {
            repository.Awaiting(r => r.FindAsync(null)).Should().Throw<ArgumentNullException>();
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
            repository.Create(model);

            // Assert
            A.CallTo(() => collection.InsertOne(model, null, default)).MustHaveHappenedOnceExactly();
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
            repository.Create(models);

            // Assert
            A.CallTo(() => collection.InsertMany(models, null, default)).MustHaveHappenedOnceExactly();
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
            repository.CreateAsync(model);

            // Assert
            A.CallTo(() => collection.InsertOneAsync(model, null, default)).MustHaveHappenedOnceExactly();
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
            repository.CreateAsync(models);

            // Assert
            A.CallTo(() => collection.InsertManyAsync(models, null, default)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Update_Any_ReplaceOneCalled()
        {
            string id = "AnyId";
            var model = new AnyModel
            {
                AnyString = "AnyString"
            };

            repository.Update(id, model);

            A.CallTo(() => collection.ReplaceOne(A<Expression<Func<AnyModel, bool>>>.Ignored, model)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Update_MissingId_ThrowsArgumentNullException()
        {
            repository.Invoking(r => r.Update(null, new AnyModel())).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdateAsync_Any_ReplaceOneAsyncCalled()
        {
            string id = "AnyId";
            var model = new AnyModel
            {
                AnyString = "AnyString"
            };

            repository.UpdateAsync(id, model);

            A.CallTo(() => collection.ReplaceOneAsync(A<Expression<Func<AnyModel, bool>>>.Ignored, model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void UpdateAsync_MissingId_ThrowsArgumentNullException()
        {
            repository.Awaiting(r => r.UpdateAsync(null, new AnyModel())).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Remove_AnyId_DeleteOneCalled()
        {
            string id = "AnyId";

            repository.Remove(id);

            A.CallTo(() => collection.DeleteOne(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Remove_MissingId_ThrowsArgumentNullException()
        {
            repository.Invoking(r => r.Remove(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RemoveAsync_AnyId_DeleteOneAsyncCalled()
        {
            string id = "AnyId";

            repository.RemoveAsync(id);

            A.CallTo(() => collection.DeleteOneAsync(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void RemoveAsync_MissingId_ThrowsArgumentNullException()
        {
            repository.Invoking(r => r.RemoveAsync(null)).Should().Throw<ArgumentNullException>();
        }
    }
}

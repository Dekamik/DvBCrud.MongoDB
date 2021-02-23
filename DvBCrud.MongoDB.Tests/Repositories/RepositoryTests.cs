using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using DvBCrud.MongoDB.Repositories.Proxies;
using DvBCrud.MongoDB.Settings;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;

namespace DvBCrud.MongoDB.Tests.Repositories
{
    public class RepositoryTests
    {
        private readonly IMongoCollectionProxy<AnyModel> collectionProxy;
        private readonly ILogger logger;
        private readonly IOptions<MongoSettings> options;
        private readonly AnyRepository repository;

        public RepositoryTests()
        {
            collectionProxy = A.Fake<IMongoCollectionProxy<AnyModel>>();
            options = A.Fake<IOptions<MongoSettings>>();
            logger = A.Fake<ILogger>();

            repository = new AnyRepository(collectionProxy, logger, options);
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
            A.CallTo(() => collectionProxy.InsertOne(model, null, default)).MustHaveHappenedOnceExactly();
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
            A.CallTo(() => collectionProxy.InsertMany(models, null, default)).MustHaveHappenedOnceExactly();
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
            A.CallTo(() => collectionProxy.InsertOneAsync(model, null, default)).MustHaveHappenedOnceExactly();
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
            A.CallTo(() => collectionProxy.InsertManyAsync(models, null, default)).MustHaveHappenedOnceExactly();
        }
    }
}

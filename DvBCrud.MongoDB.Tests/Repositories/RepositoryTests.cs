using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using DvBCrud.MongoDB.Settings;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Xunit;

namespace DvBCrud.MongoDB.Tests.Repositories
{
    public class RepositoryTests
    {
        private readonly IMongoCollection<AnyModel> collection;
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
            collection = A.Fake<IMongoCollection<AnyModel>>();
            database = A.Fake<IMongoDatabase>();
            client = A.Fake<IMongoClient>();
            logger = A.Fake<ILogger>();
            options = A.Fake<IOptions<MongoSettings>>();

            A.CallTo(() => database.GetCollection<AnyModel>(mongoSettings.CollectionName, null)).Returns(collection);
            A.CallTo(() => client.GetDatabase(mongoSettings.DatabaseName, null)).Returns(database);
            A.CallTo(() => options.Value).Returns(mongoSettings);

            repository = new AnyRepository(client, logger, options);

            // TODO: Find a way to assert that extension methods have been called
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
    }
}

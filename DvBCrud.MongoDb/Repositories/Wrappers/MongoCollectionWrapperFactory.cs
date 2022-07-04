using System.Diagnostics.CodeAnalysis;
using MongoDB.Driver;

namespace DvBCrud.MongoDB.Repositories.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class MongoCollectionWrapperFactory : IMongoCollectionWrapperFactory
    {
        private readonly IMongoClient _client;
        private readonly string _databaseName;

        public MongoCollectionWrapperFactory(IMongoClient client, string databaseName)
        {
            _databaseName = databaseName;
            _client = client;
        }
        
        public IMongoCollectionWrapper<T> Create<T>()
        {
            var database = _client.GetDatabase(_databaseName);
            var collection = database.GetCollection<T>(typeof(T).Name);
            return new MongoCollectionWrapper<T>(collection);
        }
    }
}
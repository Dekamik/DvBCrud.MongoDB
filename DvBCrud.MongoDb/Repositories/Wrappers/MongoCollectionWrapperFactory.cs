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
        
        public IMongoCollectionWrapper<TModel> Create<TModel>()
        {
            var database = _client.GetDatabase(_databaseName);
            var collection = database.GetCollection<TModel>(typeof(TModel).Name);
            return new MongoCollectionWrapper<TModel>(collection);
        }
    }
}
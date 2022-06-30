using System.Diagnostics.CodeAnalysis;
using MongoDB.Driver;

namespace DvBCrud.MongoDB.Repositories.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class MongoCollectionWrapperFactory : IMongoCollectionWrapperFactory
    {
        public IMongoCollectionWrapper<T> Create<T>(IMongoCollection<T> collection) =>
            new MongoCollectionWrapper<T>(collection);
    }
}
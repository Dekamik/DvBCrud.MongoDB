using MongoDB.Driver;

namespace DvBCrud.MongoDb.Repositories.Wrappers
{
    public interface IMongoCollectionWrapperFactory
    {
        IMongoCollectionWrapper<T> Create<T>();
    }
}
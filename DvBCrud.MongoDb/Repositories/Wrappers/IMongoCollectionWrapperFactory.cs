using MongoDB.Driver;

namespace DvBCrud.MongoDB.Repositories.Wrappers
{
    public interface IMongoCollectionWrapperFactory
    {
        IMongoCollectionWrapper<T> Create<T>(IMongoCollection<T> collection);
    }
}
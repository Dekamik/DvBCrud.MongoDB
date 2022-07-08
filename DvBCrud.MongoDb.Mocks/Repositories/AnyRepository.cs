using DvBCrud.MongoDb.Mocks.Models;
using DvBCrud.MongoDb.Repositories;
using DvBCrud.MongoDb.Repositories.Wrappers;
using MongoDB.Driver;

namespace DvBCrud.MongoDb.Mocks.Repositories
{
    public class AnyRepository : Repository<AnyDataModel>, IAnyRepository
    {
        public AnyRepository(IMongoCollectionWrapperFactory factory) : base(factory)
        {

        }
    }
}

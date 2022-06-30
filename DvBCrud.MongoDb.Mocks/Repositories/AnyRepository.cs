using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Repositories;
using DvBCrud.MongoDB.Repositories.Wrappers;
using DvBCrud.MongoDB.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DvBCrud.MongoDB.Mocks.Repositories
{
    public class AnyRepository : Repository<AnyModel>, IAnyRepository
    {
        public AnyRepository(IMongoClient client, IOptions<MongoSettings> options, IMongoCollectionWrapperFactory factory) : base(client, options, factory)
        {

        }
    }
}

using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Repositories;
using DvBCrud.MongoDB.Repositories.Proxies;
using DvBCrud.MongoDB.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DvBCrud.MongoDB.Mocks.Repositories
{
    public class AnyRepository : Repository<AnyModel>, IAnyRepository
    {
        public AnyRepository(IMongoCollectionProxy<AnyModel> collectionProxy, ILogger logger, IOptions<MongoSettings> options) : base(collectionProxy, logger, options)
        {

        }
    }
}

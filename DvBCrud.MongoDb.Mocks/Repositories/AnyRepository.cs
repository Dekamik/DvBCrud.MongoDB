using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Repositories;
using DvBCrud.MongoDB.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DvBCrud.MongoDB.Mocks.Repositories
{
    public class AnyRepository : Repository<AnyModel>, IAnyRepository
    {
        public AnyRepository(IMongoClient client, ILogger logger, IOptions<MongoSettings> options) : base(client, logger, options)
        {

        }
    }
}

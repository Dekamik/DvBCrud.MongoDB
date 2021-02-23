using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories.Proxies;
using DvBCrud.MongoDB.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DvBCrud.MongoDB.Repositories
{
    public abstract class Repository<TModel> : IRepository<TModel>
        where TModel : BaseModel
    {
        protected readonly IMongoCollectionProxy<TModel> collectionProxy;

        protected readonly ILogger logger;

        public Repository(IMongoCollectionProxy<TModel> collectionProxy, ILogger logger, IOptions<MongoSettings> options)
        {
            this.logger = logger;
            this.collectionProxy = collectionProxy;
        }

        public IEnumerable<TModel> Find() => collectionProxy.Find(m => true).ToEnumerable();

        public TModel Find(string id) => collectionProxy.Find(m => m.Id == id).FirstOrDefault();

        public async Task<IEnumerable<TModel>> FindAsync() => (await collectionProxy.FindAsync(m => true)).ToEnumerable();

        public async Task<TModel> FindAsync(string id)
        {
            var cursor = await collectionProxy.FindAsync(m => m.Id == id);
            return await cursor.FirstOrDefaultAsync();
        }

        public void Create(TModel data) => collectionProxy.InsertOne(data);

        public void Create(IEnumerable<TModel> data) => collectionProxy.InsertMany(data);

        public Task CreateAsync(TModel data) => collectionProxy.InsertOneAsync(data);

        public Task CreateAsync(IEnumerable<TModel> data) => collectionProxy.InsertManyAsync(data);

        public void Update(string id, TModel data) => collectionProxy.ReplaceOne(d => d.Id == id, data);

        public Task UpdateAsync(string id, TModel data) => collectionProxy.ReplaceOneAsync(d => d.Id == id, data);

        public void Remove(string id) => collectionProxy.DeleteOne(d => d.Id == id);

        public Task RemoveAsync(string id) => collectionProxy.DeleteOneAsync(d => d.Id == id);
    }
}

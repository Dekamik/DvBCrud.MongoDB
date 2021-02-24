using DvBCrud.MongoDB.Models;
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
        protected readonly IMongoCollectionProxy<TModel> collection;

        protected readonly ILogger logger;

        public Repository(IMongoClient client, ILogger logger, IOptions<MongoSettings> options)
        {
            this.logger = logger;

            var database = client.GetDatabase(options.Value.DatabaseName);
            collection = (IMongoCollectionProxy<TModel>)database.GetCollection<TModel>(options.Value.CollectionName);
        }

        public IEnumerable<TModel> Find() => collection.Find(m => true).ToEnumerable();

        public TModel Find(string id) => collection.Find(m => m.Id == id).FirstOrDefault();

        public async Task<IEnumerable<TModel>> FindAsync() => (await collection.FindAsync(m => true)).ToEnumerable();

        public async Task<TModel> FindAsync(string id)
        {
            var cursor = await collection.FindAsync(m => m.Id == id);
            return await cursor.FirstOrDefaultAsync();
        }

        public void Create(TModel data) => collection.InsertOne(data);

        public void Create(IEnumerable<TModel> data) => collection.InsertMany(data);

        public Task CreateAsync(TModel data) => collection.InsertOneAsync(data);

        public Task CreateAsync(IEnumerable<TModel> data) => collection.InsertManyAsync(data);

        public void Update(string id, TModel data) => collection.ReplaceOne(d => d.Id == id, data);

        public Task UpdateAsync(string id, TModel data) => collection.ReplaceOneAsync(d => d.Id == id, data);

        public void Remove(string id) => collection.DeleteOne(d => d.Id == id);

        public Task RemoveAsync(string id) => collection.DeleteOneAsync(d => d.Id == id);
    }
}

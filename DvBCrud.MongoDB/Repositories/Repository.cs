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
        protected readonly IMongoCollection<TModel> collection;

        protected readonly ILogger logger;

        public Repository(IMongoClient client, ILogger logger, IOptions<MongoSettings> options)
        {
            this.logger = logger;

            var database = client.GetDatabase(options.Value.DatabaseName);
            collection = database.GetCollection<TModel>(options.Value.CollectionName);
        }

        public IEnumerable<TModel> Find() => collection.Find(m => true).ToEnumerable();

        public TModel Find(string id) 
        {
            return collection.FindSync(m => m.Id == id).Current.FirstOrDefault();
        }

        public async Task<IEnumerable<TModel>> FindAsync() => (await collection.FindAsync(m => true)).ToEnumerable();

        public async Task<TModel> FindAsync(string id)
        {
            var cursor = await collection.FindAsync(m => m.Id == id);
            return await cursor.FirstOrDefaultAsync();
        }

        public void Create(TModel model) => collection.InsertOne(model);

        public void Create(IEnumerable<TModel> model) => collection.InsertMany(model);

        public Task CreateAsync(TModel model) => collection.InsertOneAsync(model);

        public Task CreateAsync(IEnumerable<TModel> model) => collection.InsertManyAsync(model);

        public void Update(string id, TModel model) => collection.ReplaceOne(m => m.Id == id, model);

        public Task UpdateAsync(string id, TModel model) => collection.ReplaceOneAsync(m => m.Id == id, model);

        public void Remove(string id) => collection.DeleteOne(m => m.Id == id);

        public Task RemoveAsync(string id) => collection.DeleteOneAsync(m => m.Id == id);
    }
}

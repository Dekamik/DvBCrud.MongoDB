using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DvBCrud.MongoDB.Repositories
{
    public abstract class Repository<TModel> : IRepository<TModel>
        where TModel : BaseModel
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly IMongoCollectionProxy<TModel> Collection;

        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly ILogger Logger;

        protected Repository(IMongoClient client, ILogger logger, IOptions<MongoSettings> options)
        {
            Logger = logger;

            var database = client.GetDatabase(options.Value.DatabaseName);
            Collection = (IMongoCollectionProxy<TModel>)database.GetCollection<TModel>(typeof(TModel).FullName);
        }

        public IEnumerable<TModel> Find() => Collection.Find(m => true).ToEnumerable();

        public TModel Find(string id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            return Collection.Find(m => m.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<TModel>> FindAsync() => (await Collection.FindAsync(m => true)).ToEnumerable();

        public async Task<TModel> FindAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            var cursor = await Collection.FindAsync(m => m.Id == id);
            return await cursor.FirstOrDefaultAsync();
        }

        public void Create(TModel data) => Collection.InsertOne(data);

        public void Create(IEnumerable<TModel> data) => Collection.InsertMany(data);

        public Task CreateAsync(TModel data) => Collection.InsertOneAsync(data);

        public Task CreateAsync(IEnumerable<TModel> data) => Collection.InsertManyAsync(data);

        public void Update(string id, TModel data)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            Collection.ReplaceOne(d => d.Id == id, data);
        }

        public Task UpdateAsync(string id, TModel data) 
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            return Collection.ReplaceOneAsync(d => d.Id == id, data); 
        }

        public void Remove(string id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            Collection.DeleteOne(d => d.Id == id);
        }

        public Task RemoveAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            return Collection.DeleteOneAsync(d => d.Id == id);
        }
    }
}

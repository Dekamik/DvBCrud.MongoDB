using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;

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

        public TModel Find(string id) => collection.Find(m => m.Id == id).FirstOrDefault();

        public void Create(TModel model) => collection.InsertOne(model);

        public void Update(string id, TModel model) => collection.ReplaceOne(m => m.Id == id, model);

        public void Remove(string id) => collection.DeleteOne(m => m.Id == id);
    }
}

using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
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

        public TModel Create(TModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TModel> Get()
        {
            throw new NotImplementedException();
        }

        public TModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, TModel model)
        {
            throw new NotImplementedException();
        }
    }
}

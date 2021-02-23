using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DvBCrud.MongoDB.Repositories.Proxies
{
    public class MongoCollectionProxy<TModel> : IMongoCollectionProxy<TModel>
    {
        private readonly IMongoCollection<TModel> collection;

        public MongoCollectionProxy(IMongoCollection<TModel> collection)
        {
            this.collection = collection;
        }

        public DeleteResult DeleteOne(Expression<Func<TModel, bool>> filter, DeleteOptions options, CancellationToken cancellationToken = default) => collection.DeleteOne(filter, options, cancellationToken);

        public Task<DeleteResult> DeleteOneAsync(Expression<Func<TModel, bool>> filter, DeleteOptions options, CancellationToken cancellationToken = default) => collection.DeleteOneAsync(filter, options, cancellationToken);

        public IFindFluent<TModel, TModel> Find(Expression<Func<TModel, bool>> filter, FindOptions options = null) => collection.Find(filter, options);

        public Task<IAsyncCursor<TModel>> FindAsync(Expression<Func<TModel, bool>> filter, FindOptions<TModel, TModel> options = null, CancellationToken cancellationToken = default) => collection.FindAsync(filter, options, cancellationToken);

        public void InsertMany(IEnumerable<TModel> model, InsertManyOptions options = null, CancellationToken cancellationToken = default) => collection.InsertMany(model, options, cancellationToken);

        public Task InsertManyAsync(IEnumerable<TModel> model, InsertManyOptions options = null, CancellationToken cancellationToken = default) => collection.InsertManyAsync(model, options, cancellationToken);

        public void InsertOne(TModel model, InsertOneOptions options = null, CancellationToken cancellationToken = default) => collection.InsertOne(model, options, cancellationToken);

        public Task InsertOneAsync(TModel model, InsertOneOptions options = null, CancellationToken cancellationToken = default) => collection.InsertOneAsync(model, options, cancellationToken);

        public ReplaceOneResult ReplaceOne(Expression<Func<TModel, bool>> filter, TModel replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default) => collection.ReplaceOne(filter, replacement, options, cancellationToken);

        public Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<TModel, bool>> filter, TModel replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default) => collection.ReplaceOneAsync(filter, replacement, options, cancellationToken);
    }
}

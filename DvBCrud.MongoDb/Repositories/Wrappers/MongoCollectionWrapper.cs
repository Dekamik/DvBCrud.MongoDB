using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DvBCrud.MongoDB.Repositories.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class MongoCollectionWrapper<TModel> : IMongoCollectionWrapper<TModel>
    {
        private readonly IMongoCollection<TModel> _collection;
        
        public MongoCollectionWrapper(IMongoCollection<TModel> collection)
        {
            _collection = collection;
        }

        public IFindFluent<TModel, TModel> Find(Expression<Func<TModel, bool>> filter) => _collection.Find(filter);

        public Task<IAsyncCursor<TModel>> FindAsync(Expression<Func<TModel, bool>> filter) =>
            _collection.FindAsync(filter);

        public void InsertOne(TModel data) => _collection.InsertOne(data);

        public void InsertMany(IEnumerable<TModel> data) => _collection.InsertMany(data);

        public Task InsertOneAsync(TModel data) => _collection.InsertOneAsync(data);

        public Task InsertManyAsync(IEnumerable<TModel> data) => _collection.InsertManyAsync(data);

        public ReplaceOneResult ReplaceOne(Expression<Func<TModel, bool>> filter, TModel replacement) =>
            _collection.ReplaceOne(filter, replacement);

        public Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<TModel, bool>> filter, TModel replacement) =>
            _collection.ReplaceOneAsync(filter, replacement);

        public DeleteResult DeleteOne(Expression<Func<TModel, bool>> filter) => _collection.DeleteOne(filter);

        public Task<DeleteResult> DeleteOneAsync(Expression<Func<TModel, bool>> filter) =>
            _collection.DeleteOneAsync(filter);
    }
}
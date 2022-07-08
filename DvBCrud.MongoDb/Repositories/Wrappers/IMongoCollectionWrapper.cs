using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DvBCrud.MongoDb.Repositories.Wrappers
{
    public interface IMongoCollectionWrapper<TModel>
    {
        IFindFluent<TModel, TModel> Find(Expression<Func<TModel, bool>> filter);

        Task<IAsyncCursor<TModel>> FindAsync(Expression<Func<TModel, bool>> filter);

        ReplaceOneResult ReplaceOne(Expression<Func<TModel, bool>> filter, TModel replacement);

        Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<TModel, bool>> filter, TModel replacement);

        DeleteResult DeleteOne(Expression<Func<TModel, bool>> filter);

        Task<DeleteResult> DeleteOneAsync(Expression<Func<TModel, bool>> filter);
        void InsertOne(TModel data);
        void InsertMany(IEnumerable<TModel> data);
        Task InsertOneAsync(TModel data);
        Task InsertManyAsync(IEnumerable<TModel> data);
    }
}

using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DvBCrud.MongoDB.Repositories
{
    public interface IMongoCollectionProxy<TModel> : IMongoCollection<TModel>
    {
        IFindFluent<TModel, TModel> Find(Expression<Func<TModel, bool>> filter);

        Task<IAsyncCursor<TModel>> FindAsync(Expression<Func<TModel, bool>> filter);

        ReplaceOneResult ReplaceOne(Expression<Func<TModel, bool>> filter, TModel replacement);

        Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<TModel, bool>> filter, TModel replacement);

        DeleteResult DeleteOne(Expression<Func<TModel, bool>> filter);

        Task<DeleteResult> DeleteOneAsync(Expression<Func<TModel, bool>> filter);
    }
}

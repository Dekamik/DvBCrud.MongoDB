using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DvBCrud.MongoDB.Mocks
{
    public interface IMongoCollectionProxy<TModel> : IMongoCollection<TModel>
    {
        IFindFluent<TModel, TModel> Find(Expression<Func<TModel, bool>> filter, FindOptions options = null);

        Task<IAsyncCursor<TModel>> FindAsync(Expression<Func<TModel, bool>> filter, FindOptions<TModel, TModel> options = null, CancellationToken cancellationToken = default);

        ReplaceOneResult ReplaceOne(Expression<Func<TModel, bool>> filter, TModel replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default);

        Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<TModel, bool>> filter, TModel replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default);

        DeleteResult DeleteOne(Expression<Func<TModel, bool>> filter, DeleteOptions options = null, CancellationToken cancellationToken = default);

        Task<DeleteResult> DeleteOneAsync(Expression<Func<TModel, bool>> filter, DeleteOptions options = null, CancellationToken cancellationToken = default);
    }
}

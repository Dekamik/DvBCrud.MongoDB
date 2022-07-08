using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.MongoDb.Repositories
{
    public interface IRepository<TModel>
    {
        IEnumerable<TModel> Find();

        TModel Find(string id);

        IEnumerable<TModel> Find(IEnumerable<string> ids);

        Task<IEnumerable<TModel>> FindAsync();

        Task<TModel> FindAsync(string id);

        Task<IEnumerable<TModel>> FindAsync(IEnumerable<string> ids);

        void Create(TModel data);

        void Create(IEnumerable<TModel> data);

        Task CreateAsync(TModel data);

        Task CreateAsync(IEnumerable<TModel> data);

        void Update(string id, TModel data);

        Task UpdateAsync(string id, TModel data);

        void Remove(string id);

        Task RemoveAsync(string id);
    }
}

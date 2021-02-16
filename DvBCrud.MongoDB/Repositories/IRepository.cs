using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.MongoDB.Repositories
{
    public interface IRepository<TModel>
    {
        IEnumerable<TModel> Find();

        TModel Find(string id);

        Task<IEnumerable<TModel>> FindAsync();

        Task<TModel> FindAsync(string id);

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

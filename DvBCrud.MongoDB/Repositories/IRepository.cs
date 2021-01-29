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

        void Create(TModel model);

        void Create(IEnumerable<TModel> models);

        Task CreateAsync(TModel model);

        Task CreateAsync(IEnumerable<TModel> models);

        void Update(string id, TModel model);

        Task UpdateAsync(string id, TModel model);

        void Remove(string id);

        Task RemoveAsync(string id);
    }
}

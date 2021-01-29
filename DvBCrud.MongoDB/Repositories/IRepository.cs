using System.Collections.Generic;

namespace DvBCrud.MongoDB.Repositories
{
    public interface IRepository<TModel>
    {
        IEnumerable<TModel> Get();

        TModel Get(string id);

        TModel Create(TModel model);

        void Update(string id, TModel model);

        void Remove(string id);
    }
}

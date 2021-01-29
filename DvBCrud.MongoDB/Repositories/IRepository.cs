using System.Collections.Generic;

namespace DvBCrud.MongoDB.Repositories
{
    public interface IRepository<TModel>
    {
        IEnumerable<TModel> Find();

        TModel Find(string id);

        void Create(TModel model);

        void Update(string id, TModel model);

        void Remove(string id);
    }
}

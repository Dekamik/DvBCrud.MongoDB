using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories.Wrappers;
using MongoDB.Driver;

namespace DvBCrud.MongoDB.Repositories
{
    public abstract class Repository<TModel> : IRepository<TModel>
        where TModel : BaseModel
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly IMongoCollectionWrapper<TModel> Collection;

        protected Repository(IMongoCollectionWrapperFactory factory)
        {
            Collection = factory.Create<TModel>();
        }

        public virtual IEnumerable<TModel> Find() => Collection.Find(m => true).ToEnumerable();

        public virtual TModel Find(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return Collection.Find(m => m.Id == id).FirstOrDefault();
        }

        public virtual IEnumerable<TModel> Find(IEnumerable<string> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));
            
            var enumerable = ids as string[] ?? ids.ToArray();
            
            if (!enumerable.Any())
                throw new ArgumentException($"{nameof(ids)} collection is empty.");
            
            return enumerable.Length == 1 ? 
                new [] { Find(enumerable.First()) } : 
                Collection.Find(m => enumerable.Contains(m.Id)).ToEnumerable();
        } 

        public virtual async Task<IEnumerable<TModel>> FindAsync() => (await Collection.FindAsync(m => true)).ToEnumerable();

        public virtual async Task<TModel> FindAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var cursor = await Collection.FindAsync(m => m.Id == id);
            return await cursor.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TModel>> FindAsync(IEnumerable<string> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var enumerable = ids as string[] ?? ids.ToArray();
            
            if (!enumerable.Any())
                throw new ArgumentException($"{nameof(ids)} collection is empty.");
            
            return enumerable.Length == 1 ? 
                new [] { await FindAsync(enumerable.First()) } : 
                (await Collection.FindAsync(m => enumerable.Contains(m.Id))).ToEnumerable();
        }

        public virtual void Create(TModel data) => Collection.InsertOne(data);

        public virtual void Create(IEnumerable<TModel> data) => Collection.InsertMany(data);

        public virtual Task CreateAsync(TModel data) => Collection.InsertOneAsync(data);

        public virtual Task CreateAsync(IEnumerable<TModel> data) => Collection.InsertManyAsync(data);

        public virtual void Update(string id, TModel data)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            Collection.ReplaceOne(d => d.Id == id, data);
        }

        public virtual Task UpdateAsync(string id, TModel data) 
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return Collection.ReplaceOneAsync(d => d.Id == id, data); 
        }

        public virtual void Remove(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            Collection.DeleteOne(d => d.Id == id);
        }

        public virtual Task RemoveAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return Collection.DeleteOneAsync(d => d.Id == id);
        }
    }
}

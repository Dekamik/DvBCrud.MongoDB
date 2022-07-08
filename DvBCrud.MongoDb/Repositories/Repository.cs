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
        where TModel : BaseDataModel
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly IMongoCollectionWrapper<TModel> Collection;

        protected Repository(IMongoCollectionWrapperFactory factory)
        {
            Collection = factory.Create<TModel>();
        }

        public virtual IEnumerable<TModel> Find() => 
            Collection.Find(m => true)
                .ToEnumerable();

        public virtual TModel Find(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return Collection.Find(m => m.Id == id)
                .FirstOrDefault();
        }

        public virtual IEnumerable<TModel> Find(IEnumerable<string> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));
            
            var enumerable = ids as string[] ?? ids.ToArray();
            
            if (!enumerable.Any())
                throw new ArgumentException($"{nameof(ids)} collection is empty.");
            
            return enumerable.Length == 1 ? 
                new []
                {
                    Find(enumerable.First())
                } : 
                Collection.Find(m => enumerable.Contains(m.Id))
                    .ToEnumerable();
        } 

        public virtual async Task<IEnumerable<TModel>> FindAsync() => 
            (await Collection.FindAsync(m => true))
            .ToEnumerable();

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
                new []
                {
                    await FindAsync(enumerable.First())
                } : 
                (await Collection.FindAsync(m => enumerable.Contains(m.Id)))
                    .ToEnumerable();
        }

        public virtual void Create(TModel data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            
            Collection.InsertOne(data);
        }

        public virtual void Create(IEnumerable<TModel> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            
            var dataArray = data as TModel[] ?? data.ToArray();
            
            if (!dataArray.Any())
                throw new ArgumentException($"{nameof(data)} collection is empty.");
            
            Collection.InsertMany(dataArray);
        }

        public virtual Task CreateAsync(TModel data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            
            return Collection.InsertOneAsync(data);
        }

        public virtual Task CreateAsync(IEnumerable<TModel> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            
            var dataArray = data as TModel[] ?? data.ToArray();
            
            if (!dataArray.Any())
                throw new ArgumentException($"{nameof(data)} collection is empty.");
            
            return Collection.InsertManyAsync(dataArray);
        }

        public virtual void Update(string id, TModel data)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            data.Id = id;

            var result = Collection.ReplaceOne(d => d.Id == id, data);

            if (result.MatchedCount == 0 || result.ModifiedCount == 0)
                throw new KeyNotFoundException($"{typeof(TModel).Name} {id} not found");
        }

        public virtual async Task UpdateAsync(string id, TModel data) 
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            data.Id = id;

            var result = await Collection.ReplaceOneAsync(d => d.Id == id, data);
            
            if (result.ModifiedCount == 0)
                throw new KeyNotFoundException($"{typeof(TModel).Name} {id} not found");
        }

        public virtual void Remove(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var result = Collection.DeleteOne(d => d.Id == id);

            if (result.DeletedCount == 0)
                throw new KeyNotFoundException($"{typeof(TModel).Name} {id} not found");
        }

        public virtual async Task RemoveAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var result = await Collection.DeleteOneAsync(d => d.Id == id);
            
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException($"{typeof(TModel).Name} {id} not found");
        }
    }
}

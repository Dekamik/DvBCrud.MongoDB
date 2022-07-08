using DvBCrud.Common.Services.Conversion;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;

namespace DvBCrud.MongoDb.Services;

public abstract class Service<TDataModel, TRepository, TApiModel, TConverter> : IService<TApiModel> where TDataModel : BaseDataModel
    where TRepository : IRepository<TDataModel>
    where TConverter : IConverter<TDataModel, TApiModel>
{
    protected readonly TRepository Repository;
    protected readonly TConverter Converter;

    protected Service(TRepository repository, TConverter converter)
    {
        Converter = converter;
        Repository = repository;
    }

    public virtual IEnumerable<TApiModel> GetAll() => 
        Repository.Find()
            .Select(Converter.ToApiModel);

    public virtual async Task<IEnumerable<TApiModel>> GetAllAsync() =>
        (await Repository.FindAsync())
            .Select(Converter.ToApiModel);

    public virtual TApiModel? Get(string id)
    {
        var dataModel = Repository.Find(id);
        return Converter.ToApiModel(dataModel);
    }

    public virtual async Task<TApiModel?> GetAsync(string id)
    {
        var dataModel = await Repository.FindAsync(id);
        return Converter.ToApiModel(dataModel);
    }

    public virtual void Create(TApiModel apiModel)
    {
        if (apiModel == null)
            throw new ArgumentNullException(nameof(apiModel));

        var dataModel = Converter.ToDataModel(apiModel);
        Repository.Create(dataModel);
    }

    public virtual async Task CreateAsync(TApiModel apiModel)
    {
        if (apiModel == null)
            throw new ArgumentNullException(nameof(apiModel));

        var dataModel = Converter.ToDataModel(apiModel);
        await Repository.CreateAsync(dataModel);
    }

    public virtual void Update(string id, TApiModel apiModel)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        if (apiModel == null)
            throw new ArgumentNullException(nameof(apiModel));

        var dataModel = Converter.ToDataModel(apiModel);
        Repository.Update(id, dataModel);
    }
    
    public virtual async Task UpdateAsync(string id, TApiModel apiModel)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        if (apiModel == null)
            throw new ArgumentNullException(nameof(apiModel));

        var dataModel = Converter.ToDataModel(apiModel);
        await Repository.UpdateAsync(id, dataModel);
    }

    public virtual void Delete(string id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        
        Repository.Remove(id);
    }
    
    public virtual async Task DeleteAsync(string id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        
        await Repository.RemoveAsync(id);
    }
}
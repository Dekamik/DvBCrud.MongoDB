namespace DvBCrud.MongoDb.Services;

public interface IService<TApiModel>
{
    IEnumerable<TApiModel> GetAll();
    Task<IEnumerable<TApiModel>> GetAllAsync();
    TApiModel? Get(string id);
    Task<TApiModel?> GetAsync(string id);
    void Create(TApiModel apiModel);
    Task CreateAsync(TApiModel apiModel);
    void Update(string id, TApiModel apiModel);
    Task UpdateAsync(string id, TApiModel apiModel);
    void Delete(string id);
    Task DeleteAsync(string id);
}
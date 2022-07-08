using DvBCrud.Common.Services.Conversion;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;

namespace DvBCrud.MongoDb.Services;

public abstract class SimpleService<TDataModel, TRepository, TConverter> : Service<TDataModel, TRepository, TDataModel, TConverter>, ISimpleService<TDataModel>
    where TDataModel : BaseDataModel
    where TRepository : IRepository<TDataModel>
    where TConverter : IConverterOverride<TDataModel>
{
    protected SimpleService(TRepository repository, TConverter converter) : base(repository, converter)
    {
    }
}
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using DvBCrud.MongoDb.Services;

namespace DvBCrud.MongoDB.Mocks.Services;

public class AnyService : Service<AnyDataModel, IAnyRepository, AnyApiModel, IAnyConverter>, IAnyService
{
    public AnyService(IAnyRepository repository, IAnyConverter converter) : base(repository, converter)
    {
    }
}
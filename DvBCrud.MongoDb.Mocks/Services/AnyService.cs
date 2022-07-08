using DvBCrud.MongoDb.Mocks.Models;
using DvBCrud.MongoDb.Mocks.Repositories;
using DvBCrud.MongoDb.Services;

namespace DvBCrud.MongoDb.Mocks.Services;

public class AnyService : Service<AnyDataModel, IAnyRepository, AnyApiModel, IAnyConverter>, IAnyService
{
    public AnyService(IAnyRepository repository, IAnyConverter converter) : base(repository, converter)
    {
    }
}
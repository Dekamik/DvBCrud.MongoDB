using DvBCrud.MongoDb.Api.Controllers;
using DvBCrud.MongoDb.Mocks.Models;
using DvBCrud.MongoDb.Mocks.Services;

namespace DvBCrud.MongoDb.Mocks.Controllers.Async
{
    public class AnyAsyncController : AsyncCrudController<AnyApiModel, IAnyService>
    {
        public AnyAsyncController(IAnyService service) : base(service)
        {
        }
    }
}

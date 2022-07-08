using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Services;

namespace DvBCrud.MongoDB.Mocks.Controllers.Async
{
    public class AnyAsyncController : AsyncCrudController<AnyApiModel, IAnyService>
    {
        public AnyAsyncController(IAnyService service) : base(service)
        {
        }
    }
}

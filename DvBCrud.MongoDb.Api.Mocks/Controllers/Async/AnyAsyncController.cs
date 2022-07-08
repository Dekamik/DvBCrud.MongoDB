using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Async
{
    public class AnyAsyncController : AsyncCrudController<AnyDataModel, IAnyRepository>
    {
        public AnyAsyncController(IAnyRepository repository) : base(repository)
        {
        }
    }
}

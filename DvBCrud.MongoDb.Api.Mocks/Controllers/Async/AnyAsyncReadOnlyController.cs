using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.API.XMLJSON;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Async
{
    public class AnyAsyncReadOnlyController : AsyncCrudController<AnyModel, IAnyRepository>, IAnyAsyncReadOnlyController
    {
        public AnyAsyncReadOnlyController(IAnyRepository repository, ILogger logger) : base(repository, logger, CrudAction.Read)
        {
        }
    }
}

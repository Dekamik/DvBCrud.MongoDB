using DvBCrud.Common.Api.CrudActions;
using DvBCrud.MongoDb.Api.Controllers;
using DvBCrud.MongoDb.Mocks.Models;
using DvBCrud.MongoDb.Mocks.Services;

namespace DvBCrud.MongoDb.Mocks.Controllers.Async
{
    [AllowedActions(CrudAction.Read)]
    public class AnyAsyncReadOnlyController : AsyncCrudController<AnyApiModel, IAnyService>
    {
        public AnyAsyncReadOnlyController(IAnyService service) : base(service)
        {
        }
    }
}

using DvBCrud.Common.Api.CrudActions;
using DvBCrud.MongoDb.Api.Controllers;
using DvBCrud.MongoDb.Mocks.Models;
using DvBCrud.MongoDb.Mocks.Repositories;
using DvBCrud.MongoDb.Mocks.Services;

namespace DvBCrud.MongoDb.Mocks.Controllers.Async
{
    [AllowedActions(CrudAction.Create, CrudAction.Update, CrudAction.Delete)]
    public class AnyAsyncTestController : AsyncCrudController<AnyApiModel, IAnyService>
    {
        public AnyAsyncTestController(IAnyService service) : base(service)
        {
        }
    }
}

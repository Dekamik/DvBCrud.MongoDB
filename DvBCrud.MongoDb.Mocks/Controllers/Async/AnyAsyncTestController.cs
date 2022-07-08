using DvBCrud.Common.Api.CrudActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using DvBCrud.MongoDB.Mocks.Services;

namespace DvBCrud.MongoDB.Mocks.Controllers.Async
{
    [AllowedActions(CrudAction.Create, CrudAction.Update, CrudAction.Delete)]
    public class AnyAsyncTestController : AsyncCrudController<AnyApiModel, IAnyService>
    {
        public AnyAsyncTestController(IAnyService service) : base(service)
        {
        }
    }
}

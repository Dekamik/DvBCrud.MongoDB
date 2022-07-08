using DvBCrud.Common.Api.CrudActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Services;

namespace DvBCrud.MongoDB.Mocks.Controllers.Async
{
    [AllowedActions(CrudAction.Read)]
    public class AnyAsyncReadOnlyController : AsyncCrudController<AnyApiModel, IAnyService>
    {
        public AnyAsyncReadOnlyController(IAnyService service) : base(service)
        {
        }
    }
}

using DvBCrud.Common.Api.CrudActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Async
{
    [AllowedActions(CrudAction.Create, CrudAction.Update, CrudAction.Delete)]
    public class AnyAsyncTestController : AsyncCrudController<AnyModel, IAnyRepository>
    {
        public AnyAsyncTestController(IAnyRepository repository) : base(repository)
        {
        }
    }
}

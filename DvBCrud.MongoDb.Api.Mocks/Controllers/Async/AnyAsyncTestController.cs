using DvBCrud.MongoDB.API.CrudActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Async
{
    public class AnyAsyncTestController : AsyncCrudController<AnyModel, IAnyRepository>
    {
        public AnyAsyncTestController(IAnyRepository repository, params CrudAction[] allowedActions) : base(repository, allowedActions)
        {
        }
    }
}

using DvBCrud.MongoDB.API.CrudActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Async
{
    public class AnyAsyncReadOnlyController : AsyncCrudController<AnyModel, IAnyRepository>
    {
        public AnyAsyncReadOnlyController(IAnyRepository repository) : base(repository, CrudAction.Read)
        {
        }
    }
}

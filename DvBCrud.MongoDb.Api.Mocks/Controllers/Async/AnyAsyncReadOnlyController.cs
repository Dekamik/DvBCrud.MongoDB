using DvBCrud.Common.Api.CrudActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Async
{
    [AllowedActions(CrudAction.Read)]
    public class AnyAsyncReadOnlyController : AsyncCrudController<AnyDataModel, IAnyRepository>
    {
        public AnyAsyncReadOnlyController(IAnyRepository repository) : base(repository)
        {
        }
    }
}

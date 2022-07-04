using DvBCrud.MongoDB.API.CrudActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Sync
{
    [AllowedActions(CrudAction.Read)]
    public class AnyReadOnlyController : CrudController<AnyModel, IAnyRepository>
    {
        public AnyReadOnlyController(IAnyRepository repository) : base(repository)
        {

        }
    }
}

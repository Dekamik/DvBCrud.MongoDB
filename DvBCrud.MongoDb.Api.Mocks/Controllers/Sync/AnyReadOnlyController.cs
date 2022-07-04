using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Sync
{
    public class AnyReadOnlyController : CrudController<AnyModel, IAnyRepository>
    {
        public AnyReadOnlyController(IAnyRepository repository) : base(repository, CrudAction.Read)
        {

        }
    }
}

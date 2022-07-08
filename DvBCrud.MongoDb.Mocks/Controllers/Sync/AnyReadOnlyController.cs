using DvBCrud.Common.Api.CrudActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Services;

namespace DvBCrud.MongoDB.Mocks.Controllers.Sync
{
    [AllowedActions(CrudAction.Read)]
    public class AnyReadOnlyController : CrudController<AnyApiModel, IAnyService>
    {
        public AnyReadOnlyController(IAnyService service) : base(service)
        {

        }
    }
}

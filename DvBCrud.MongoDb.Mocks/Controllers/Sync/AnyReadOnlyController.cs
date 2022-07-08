using DvBCrud.Common.Api.CrudActions;
using DvBCrud.MongoDb.Api.Controllers;
using DvBCrud.MongoDb.Mocks.Models;
using DvBCrud.MongoDb.Mocks.Services;

namespace DvBCrud.MongoDb.Mocks.Controllers.Sync
{
    [AllowedActions(CrudAction.Read)]
    public class AnyReadOnlyController : CrudController<AnyApiModel, IAnyService>
    {
        public AnyReadOnlyController(IAnyService service) : base(service)
        {

        }
    }
}

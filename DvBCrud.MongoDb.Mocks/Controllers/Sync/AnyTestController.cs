using DvBCrud.Common.Api.CrudActions;
using DvBCrud.MongoDb.Api.Controllers;
using DvBCrud.MongoDb.Mocks.Models;
using DvBCrud.MongoDb.Mocks.Services;

namespace DvBCrud.MongoDb.Mocks.Controllers.Sync
{
    /// <summary>
    /// This is for testing only and won't work with dependency injection. 
    /// For a better example of permissions restrictions, see <see cref="AnyReadOnlyController"/>
    /// </summary>
    [AllowedActions(CrudAction.Create, CrudAction.Update, CrudAction.Delete)]
    public class AnyTestController : CrudController<AnyApiModel, IAnyService>
    {
        public AnyTestController(IAnyService service) : base(service)
        {

        }
    }
}

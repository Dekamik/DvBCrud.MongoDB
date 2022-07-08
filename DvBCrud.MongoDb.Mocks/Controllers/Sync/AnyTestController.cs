using DvBCrud.Common.Api.CrudActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Services;

namespace DvBCrud.MongoDB.Mocks.Controllers.Sync
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

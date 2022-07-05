using DvBCrud.Common.Api.CrudActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Sync
{
    /// <summary>
    /// This is for testing only and won't work with dependency injection. 
    /// For a better example of permissions restrictions, see <see cref="AnyReadOnlyController"/>
    /// </summary>
    [AllowedActions(CrudAction.Create, CrudAction.Update, CrudAction.Delete)]
    public class AnyTestController : CrudController<AnyModel, IAnyRepository>
    {
        public AnyTestController(IAnyRepository repository) : base(repository)
        {

        }
    }
}

using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Sync
{
    /// <summary>
    /// This is for testing only and won't work with dependency injection. 
    /// For a better example of permissions restrictions, see <see cref="AnyReadOnlyController"/>
    /// </summary>
    public class AnyTestController : CrudController<AnyModel, IAnyRepository>
    {
        public AnyTestController(IAnyRepository repository, params CrudAction[] actions) : base(repository, actions)
        {

        }
    }
}

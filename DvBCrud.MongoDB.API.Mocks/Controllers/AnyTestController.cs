using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.API.XMLJSON;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Mocks.Controllers
{
    /// <summary>
    /// This is for testing only and won't work with dependency injection. 
    /// For a better example of permissions restrictions, see <see cref="AnyReadOnlyController"/>
    /// </summary>
    public class AnyTestController : CRUDController<AnyModel, IAnyRepository>, IAnyTestController
    {
        public AnyTestController(IAnyRepository repository, ILogger logger, params CRUDAction[] actions) : base(repository, logger, actions)
        {

        }
    }
}

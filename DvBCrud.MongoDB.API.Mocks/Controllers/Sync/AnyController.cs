using DvBCrud.MongoDB.API.XMLJSON;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Sync
{
    public class AnyController : CRUDController<AnyModel, IAnyRepository>
    {
        public AnyController(IAnyRepository repository, ILogger logger) : base(repository, logger)
        {

        }
    }
}

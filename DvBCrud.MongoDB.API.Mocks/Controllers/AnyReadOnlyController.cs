using DvBCrud.MongoDB.API.XMLJSON;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Mocks.Controllers
{
    public class AnyReadOnlyController : CRUDController<AnyModel, IAnyRepository>, IAnyReadOnlyController
    {
        public AnyReadOnlyController(IAnyRepository repository, ILogger logger) : base(repository, logger, CRUDAction.Read)
        {

        }
    }
}

using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Services;

namespace DvBCrud.MongoDB.Mocks.Controllers.Sync
{
    public class AnyController : CrudController<AnyApiModel, IAnyService>
    {
        public AnyController(IAnyService service) : base(service)
        {

        }
    }
}

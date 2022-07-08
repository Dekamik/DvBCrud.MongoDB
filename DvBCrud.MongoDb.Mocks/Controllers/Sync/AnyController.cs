using DvBCrud.MongoDb.Api.Controllers;
using DvBCrud.MongoDb.Mocks.Models;
using DvBCrud.MongoDb.Mocks.Services;

namespace DvBCrud.MongoDb.Mocks.Controllers.Sync
{
    public class AnyController : CrudController<AnyApiModel, IAnyService>
    {
        public AnyController(IAnyService service) : base(service)
        {

        }
    }
}

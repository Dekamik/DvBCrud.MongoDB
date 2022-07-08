using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Sync
{
    public class AnyController : CrudController<AnyDataModel, IAnyRepository>
    {
        public AnyController(IAnyRepository repository) : base(repository)
        {

        }
    }
}

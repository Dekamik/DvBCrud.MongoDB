using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public class AsyncCRUDController<TModel, TRepository> : ControllerBase, IAsyncCRUDController<TModel>
        where TModel : BaseModel
        where TRepository : IRepository<TModel>
    {
        protected readonly TRepository repository;
        protected readonly ILogger logger;
        protected readonly CRUDActionPermissions crudActions;

        public AsyncCRUDController(TRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
            this.crudActions = new CRUDActionPermissions();
        }

        public AsyncCRUDController(TRepository repository, ILogger logger, params CRUDAction[] allowedActions)
        {
            this.repository = repository;
            this.logger = logger;
            this.crudActions = new CRUDActionPermissions(allowedActions);
        }

        public Task<ActionResult<TModel>> Read([FromQuery] string id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<TModel>>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Create([FromBody] TModel data)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete([FromQuery] string id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update([FromQuery] string id, [FromBody] TModel data)
        {
            throw new NotImplementedException();
        }
    }
}

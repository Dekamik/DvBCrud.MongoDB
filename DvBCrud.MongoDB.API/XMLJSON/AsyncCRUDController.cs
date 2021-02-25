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

        public async Task<IActionResult> Create([FromBody] TModel data)
        {
            if (!crudActions.IsActionAllowed(CRUDAction.Create))
            {
                return Forbidden();
            }

            await repository.CreateAsync(data);

            return Ok();
        }

        public async Task<ActionResult<TModel>> Read([FromQuery] string id)
        {
            if (!crudActions.IsActionAllowed(CRUDAction.Read))
            {
                return Forbidden();
            }

            TModel model = await repository.FindAsync(id);

            return Ok(model);
        }

        public async Task<ActionResult<IEnumerable<TModel>>> ReadAll()
        {
            if (!crudActions.IsActionAllowed(CRUDAction.Read))
            {
                return Forbidden();
            }

            IEnumerable<TModel> models = await repository.FindAsync();

            return Ok(models);
        }

        public async Task<IActionResult> Update([FromQuery] string id, [FromBody] TModel data)
        {
            if (!crudActions.IsActionAllowed(CRUDAction.Update))
            {
                return Forbidden();
            }

            await repository.UpdateAsync(id, data);

            return Ok();
        }

        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            if (!crudActions.IsActionAllowed(CRUDAction.Delete))
            {
                return Forbidden();
            }

            await repository.RemoveAsync(id);

            return Ok();
        }

        protected ObjectResult Forbidden() => StatusCode(403, $"Action forbidden on {nameof(TModel)}");
    }
}

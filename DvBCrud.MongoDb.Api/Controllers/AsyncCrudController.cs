using System.Collections.Generic;
using System.Threading.Tasks;
using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Controllers
{
    public abstract class AsyncCrudController<TModel, TRepository> : ControllerBase
        where TModel : BaseModel
        where TRepository : IRepository<TModel>
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly TRepository Repository;

        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly CrudActionPermissions CrudActions;

        protected AsyncCrudController(TRepository repository)
        {
            Repository = repository;
            CrudActions = new CrudActionPermissions();
        }

        protected AsyncCrudController(TRepository repository, params CrudAction[] allowedActions)
        {
            Repository = repository;
            CrudActions = new CrudActionPermissions(allowedActions);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
            {
                return Forbidden();
            }

            await Repository.CreateAsync(data);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TModel>> Read(string id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden();
            }

            var model = await Repository.FindAsync(id);

            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TModel>>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden();
            }

            var models = await Repository.FindAsync();

            return Ok(models);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] TModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
            {
                return Forbidden();
            }

            await Repository.UpdateAsync(id, data);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Delete))
            {
                return Forbidden();
            }

            await Repository.RemoveAsync(id);

            return Ok();
        }

        protected ObjectResult Forbidden() => StatusCode(403, $"Action forbidden on {nameof(TModel)}");
    }
}

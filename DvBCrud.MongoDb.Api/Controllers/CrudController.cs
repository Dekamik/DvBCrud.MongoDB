using System.Collections.Generic;
using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class CrudController<TModel, TRepository> : ControllerBase
        where TModel : BaseModel
        where TRepository : IRepository<TModel>
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly TRepository Repository;

        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly CrudActionPermissions CrudActions;

        protected CrudController(TRepository repository)
        {
            Repository = repository;
            CrudActions = new CrudActionPermissions();
        }

        protected CrudController(TRepository repository, params CrudAction[] allowedActions)
        {
            Repository = repository;
            CrudActions = new CrudActionPermissions(allowedActions);
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] TModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
            {
                return Forbidden();
            }

            Repository.Create(data);

            return Ok();
        }

        [HttpGet("{id}")]
        public virtual ActionResult<TModel> Read(string id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden();
            }

            var entity = Repository.Find(id);

            return Ok(entity);
        }

        [HttpGet]
        public virtual ActionResult<IEnumerable<TModel>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden();
            }

            var entities = Repository.Find();

            return Ok(entities);
        }

        [HttpPut("{id}")]
        public virtual IActionResult Update(string id, [FromBody] TModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
            {
                return Forbidden();
            }

            Repository.Update(id, data);

            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(string id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Delete))
            {
                return Forbidden();
            }

            Repository.Remove(id);

            return Ok();
        }

        protected ObjectResult Forbidden() => StatusCode(403, $"Action forbidden on {nameof(TModel)}");
    }
}

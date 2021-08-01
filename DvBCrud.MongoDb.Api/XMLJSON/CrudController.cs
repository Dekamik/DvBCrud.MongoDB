using System.Collections.Generic;
using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    [ApiController]
    [Route("[controller]")]
    public abstract class CrudController<TModel, TRepository> : ControllerBase, ICrudController<TModel>
        where TModel : BaseModel
        where TRepository : IRepository<TModel>
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly TRepository Repository;
        
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly ILogger Logger;
        
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly CrudActionPermissions CrudActions;

        protected CrudController(TRepository repository, ILogger logger)
        {
            Repository = repository;
            Logger = logger;
            CrudActions = new CrudActionPermissions();
        }

        protected CrudController(TRepository repository, ILogger logger, params CrudAction[] allowedActions)
        {
            Repository = repository;
            Logger = logger;
            CrudActions = new CrudActionPermissions(allowedActions);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
            {
                return Forbidden();
            }

            Repository.Create(data);

            return Ok();
        }

        [HttpGet, Route("{id}")]
        public ActionResult<TModel> Read([FromQuery] string id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden();
            }

            TModel entity = Repository.Find(id);

            return Ok(entity);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TModel>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden();
            }

            IEnumerable<TModel> entities = Repository.Find();

            return Ok(entities);
        }

        [HttpPut, Route("{id}")]
        public IActionResult Update([FromQuery] string id, [FromBody] TModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
            {
                return Forbidden();
            }

            Repository.Update(id, data);

            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public IActionResult Delete([FromQuery] string id)
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

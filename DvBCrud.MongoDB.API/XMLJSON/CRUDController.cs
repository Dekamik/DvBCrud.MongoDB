using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public abstract class CRUDController<TModel, TRepository> : ControllerBase, ICRUDController<TModel>
        where TModel : BaseModel
        where TRepository : IRepository<TModel>
    {
        protected readonly TRepository repository;
        protected readonly ILogger logger;
        protected readonly CRUDActionPermissions crudActions;

        public CRUDController(TRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
            this.crudActions = new CRUDActionPermissions();
        }

        public CRUDController(TRepository repository, ILogger logger, params CRUDAction[] allowedActions)
        {
            this.repository = repository;
            this.logger = logger;
            this.crudActions = new CRUDActionPermissions(allowedActions);
        }

        public IActionResult Create([FromBody] TModel data)
        {
            if (!crudActions.IsActionAllowed(CRUDAction.Create))
            {
                return Forbidden();
            }

            repository.Create(data);

            return Ok();
        }

        public ActionResult<TModel> Read([FromQuery] string id)
        {
            if (!crudActions.IsActionAllowed(CRUDAction.Read))
            {
                return Forbidden();
            }

            TModel entity = repository.Find(id);

            return Ok(entity);
        }

        public ActionResult<IEnumerable<TModel>> ReadAll()
        {
            if (!crudActions.IsActionAllowed(CRUDAction.Read))
            {
                return Forbidden();
            }

            IEnumerable<TModel> entities = repository.Find();

            return Ok(entities);
        }

        public IActionResult Update([FromQuery] string id, [FromBody] TModel data)
        {
            if (!crudActions.IsActionAllowed(CRUDAction.Update))
            {
                return Forbidden();
            }

            repository.Update(id, data);

            return Ok();
        }

        public IActionResult Delete([FromQuery] string id)
        {
            if (!crudActions.IsActionAllowed(CRUDAction.Delete))
            {
                return Forbidden();
            }

            repository.Remove(id);

            return Ok();
        }

        protected ObjectResult Forbidden() => StatusCode(403, $"Action forbidden on {nameof(TModel)}");
    }
}

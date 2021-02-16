using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public abstract class CRUDController<TModel, TRepository> : ControllerBase, ICRUDController<TModel>
        where TModel : BaseModel
        where TRepository : IRepository<TModel>
    {
        protected readonly TRepository repository;
        protected readonly ILogger logger;
        protected readonly IEnumerable<CRUDAction> allowedActions;

        public CRUDController(TRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public CRUDController(TRepository repository, ILogger logger, params CRUDAction[] allowedActions)
        {
            this.repository = repository;
            this.logger = logger;
            this.allowedActions = allowedActions;
        }

        public bool IsActionAllowed(CRUDAction action)
        {
            if (allowedActions == null)
            {
                return true;
            }

            return allowedActions.Contains(action);
        }

        public IActionResult Create([FromBody] TModel data)
        {
            if (!IsActionAllowed(CRUDAction.Create))
            {
                return Forbidden();
            }

            repository.Create(data);

            return Ok();
        }

        public ActionResult<TModel> Read([FromQuery] string id)
        {
            if (!IsActionAllowed(CRUDAction.Read))
            {
                return Forbidden();
            }

            TModel entity = repository.Find(id);

            return Ok(entity);
        }

        public ActionResult<IEnumerable<TModel>> ReadAll()
        {
            if (!IsActionAllowed(CRUDAction.Read))
            {
                return Forbidden();
            }

            IEnumerable<TModel> entities = repository.Find();

            return Ok(entities);
        }

        public IActionResult Update([FromQuery] string id, [FromBody] TModel data)
        {
            if (!IsActionAllowed(CRUDAction.Update))
            {
                return Forbidden();
            }

            repository.Update(id, data);

            return Ok();
        }

        public IActionResult Delete([FromQuery] string id)
        {
            if (!IsActionAllowed(CRUDAction.Delete))
            {
                return Forbidden();
            }

            repository.Remove(id);

            return Ok();
        }

        protected ObjectResult Forbidden() => StatusCode(403, $"Action forbidden on {nameof(TModel)}");
    }
}

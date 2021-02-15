using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public abstract class CRUDController<TEntity, TRepository> : ControllerBase, ICRUDController<TEntity>
        where TEntity : BaseModel
        where TRepository : IRepository<TEntity>
    {
        protected readonly TRepository repository;
        protected readonly ILogger logger;
        protected readonly IEnumerable<CRUDAction> allowedActions;

        public CRUDController(TRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
            allowedActions = null;
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

        [HttpGet, Route("{id}")]
        public ActionResult<TEntity> Read(string id)
        {
            if (!IsActionAllowed(CRUDAction.Read))
            {
                return StatusCode(403, $"{nameof(Read)} access denied on {nameof(TEntity)}");
            }

            TEntity entity = repository.Find(id);

            return Ok(entity);
        }
    }
}

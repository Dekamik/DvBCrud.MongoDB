﻿using System;
using System.Collections.Generic;
using System.Reflection;
using DvBCrud.MongoDB.API.CrudActions;
using DvBCrud.MongoDB.API.Swagger;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        protected readonly CrudAction[] CrudActions;

        protected CrudController(TRepository repository)
        {
            Repository = repository;
            CrudActions = GetType().GetCustomAttribute<AllowedActionsAttribute>()?.AllowedActions ?? Array.Empty<CrudAction>();
        }

        [HttpPost]
        [SwaggerDocsFilter(CrudAction.Create)]
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
        [SwaggerDocsFilter(CrudAction.Read)]
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
        [SwaggerDocsFilter(CrudAction.Read)]
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
        [SwaggerDocsFilter(CrudAction.Update)]
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
        [SwaggerDocsFilter(CrudAction.Delete)]
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

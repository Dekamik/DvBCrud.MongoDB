using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using DvBCrud.Common.Api.Controllers;
using DvBCrud.Common.Api.CrudActions;
using DvBCrud.Common.Api.Swagger;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.MongoDB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class CrudController<TModel, TRepository> : CrudControllerBase<TModel>
        where TModel : BaseDataModel
        where TRepository : IRepository<TModel>
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly TRepository Repository;

        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly CrudAction[] CrudActions;

        protected CrudController(TRepository repository)
        {
            Repository = repository;
            CrudActions = GetType().GetCustomAttribute<AllowedActionsAttribute>()?.AllowedActions ?? 
                          Array.Empty<CrudAction>();
        }

        [HttpPost]
        [SwaggerDocsFilter(CrudAction.Create)]
        public virtual IActionResult Create([FromBody] TModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
                return NotAllowed(HttpMethod.Post.Method);

            try
            {
                Repository.Create(data);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [SwaggerDocsFilter(CrudAction.Read)]
        public virtual ActionResult<TModel> Read(string id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
                return NotAllowed(HttpMethod.Get.Method);

            try
            {
                var entity = Repository.Find(id);
                return Ok(entity);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [SwaggerDocsFilter(CrudAction.Read)]
        public virtual ActionResult<IEnumerable<TModel>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
                return NotAllowed(HttpMethod.Get.Method);
            
            var entities = Repository.Find();
            return Ok(entities);
        }

        [HttpPut("{id}")]
        [SwaggerDocsFilter(CrudAction.Update)]
        public virtual IActionResult Update(string id, [FromBody] TModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
                return NotAllowed(HttpMethod.Put.Method);

            try
            {
                Repository.Update(id, data);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [SwaggerDocsFilter(CrudAction.Delete)]
        public virtual IActionResult Delete(string id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Delete))
                return NotAllowed(HttpMethod.Delete.Method);

            try
            {
                Repository.Remove(id);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

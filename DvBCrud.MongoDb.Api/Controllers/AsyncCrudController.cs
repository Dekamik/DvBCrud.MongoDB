using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using DvBCrud.Common.Api.Controllers;
using DvBCrud.Common.Api.CrudActions;
using DvBCrud.Common.Api.Swagger;
using DvBCrud.MongoDB.Models;
using DvBCrud.MongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.MongoDB.API.Controllers
{
    public abstract class AsyncCrudController<TModel, TRepository> : CrudControllerBase<TModel>
        where TModel : BaseModel
        where TRepository : IRepository<TModel>
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly TRepository Repository;

        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly CrudAction[] CrudActions;

        protected AsyncCrudController(TRepository repository)
        {
            Repository = repository;
            CrudActions = GetType().GetCustomAttribute<AllowedActionsAttribute>()?.AllowedActions ?? Array.Empty<CrudAction>();
        }

        [HttpPost]
        [SwaggerDocsFilter(CrudAction.Create)]
        public async Task<IActionResult> Create([FromBody] TModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
                return NotAllowed(HttpMethod.Delete.Method);

            try
            {
                await Repository.CreateAsync(data);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [SwaggerDocsFilter(CrudAction.Read)]
        public async Task<ActionResult<TModel>> Read(string id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
                return NotAllowed(HttpMethod.Delete.Method);

            try
            {
                var model = await Repository.FindAsync(id);
                return Ok(model);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [SwaggerDocsFilter(CrudAction.Read)]
        public async Task<ActionResult<IEnumerable<TModel>>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
                return NotAllowed(HttpMethod.Delete.Method);

            var models = await Repository.FindAsync();
            return Ok(models);
        }

        [HttpPut("{id}")]
        [SwaggerDocsFilter(CrudAction.Update)]
        public async Task<IActionResult> Update(string id, [FromBody] TModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
                return NotAllowed(HttpMethod.Delete.Method);

            try
            {
                await Repository.UpdateAsync(id, data);
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
        public async Task<IActionResult> Delete(string id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Delete))
                return NotAllowed(HttpMethod.Delete.Method);

            try
            {
                await Repository.RemoveAsync(id);
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

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using DvBCrud.Common.Api.Controllers;
using DvBCrud.Common.Api.CrudActions;
using DvBCrud.Common.Api.Swagger;
using DvBCrud.MongoDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.MongoDb.Api.Controllers
{
    public abstract class AsyncCrudController<TApiModel, TService> : CrudControllerBase<TApiModel>
        where TService : IService<TApiModel>
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly TService Service;

        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly CrudAction[] CrudActions;

        protected AsyncCrudController(TService service)
        {
            Service = service;
            CrudActions = GetType().GetCustomAttribute<AllowedActionsAttribute>()?.AllowedActions ?? 
                          Array.Empty<CrudAction>();
        }

        [HttpPost]
        [SwaggerDocsFilter(CrudAction.Create)]
        public async Task<IActionResult> Create([FromBody] TApiModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
                return NotAllowed(HttpMethod.Delete.Method);

            try
            {
                await Service.CreateAsync(data);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [SwaggerDocsFilter(CrudAction.Read)]
        public async Task<ActionResult<TApiModel>> Read(string id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
                return NotAllowed(HttpMethod.Delete.Method);

            try
            {
                var model = await Service.GetAsync(id);
                return Ok(model);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [SwaggerDocsFilter(CrudAction.Read)]
        public async Task<ActionResult<IEnumerable<TApiModel>>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
                return NotAllowed(HttpMethod.Delete.Method);

            var models = await Service.GetAllAsync();
            return Ok(models);
        }

        [HttpPut("{id}")]
        [SwaggerDocsFilter(CrudAction.Update)]
        public async Task<IActionResult> Update(string id, [FromBody] TApiModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
                return NotAllowed(HttpMethod.Delete.Method);

            try
            {
                await Service.UpdateAsync(id, data);
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
                await Service.DeleteAsync(id);
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

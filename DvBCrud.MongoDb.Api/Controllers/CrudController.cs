using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using DvBCrud.Common.Api.Controllers;
using DvBCrud.Common.Api.CrudActions;
using DvBCrud.Common.Api.Swagger;
using DvBCrud.MongoDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.MongoDB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class CrudController<TApiModel, TService> : CrudControllerBase<TApiModel>
        where TService : IService<TApiModel>
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly TService Service;

        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly CrudAction[] CrudActions;

        protected CrudController(TService service)
        {
            Service = service;
            CrudActions = GetType().GetCustomAttribute<AllowedActionsAttribute>()?.AllowedActions ?? 
                          Array.Empty<CrudAction>();
        }

        [HttpPost]
        [SwaggerDocsFilter(CrudAction.Create)]
        public virtual IActionResult Create([FromBody] TApiModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
                return NotAllowed(HttpMethod.Post.Method);

            try
            {
                Service.Create(data);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [SwaggerDocsFilter(CrudAction.Read)]
        public virtual ActionResult<TApiModel> Read(string id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
                return NotAllowed(HttpMethod.Get.Method);

            try
            {
                var entity = Service.Get(id);
                return Ok(entity);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [SwaggerDocsFilter(CrudAction.Read)]
        public virtual ActionResult<IEnumerable<TApiModel>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
                return NotAllowed(HttpMethod.Get.Method);
            
            var entities = Service.GetAll();
            return Ok(entities);
        }

        [HttpPut("{id}")]
        [SwaggerDocsFilter(CrudAction.Update)]
        public virtual IActionResult Update(string id, [FromBody] TApiModel data)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
                return NotAllowed(HttpMethod.Put.Method);

            try
            {
                Service.Update(id, data);
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
                Service.Delete(id);
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

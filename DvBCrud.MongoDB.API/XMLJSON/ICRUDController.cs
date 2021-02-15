using DvBCrud.MongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public interface ICRUDController<TEntity>
        where TEntity : BaseModel
    {
        bool IsActionAllowed(CRUDAction action);

        [HttpPost]
        IActionResult Create([FromBody] TEntity entity);

        [HttpGet, Route("{id}")]
        ActionResult<TEntity> Read([FromQuery] string id);

        [HttpGet]
        ActionResult<IEnumerable<TEntity>> ReadAll();

        [HttpPut, Route("{id}")]
        IActionResult Update([FromQuery] string id, [FromBody] TEntity entity);

        [HttpDelete, Route("{id}")]
        IActionResult Delete([FromQuery] string id);
    }
}

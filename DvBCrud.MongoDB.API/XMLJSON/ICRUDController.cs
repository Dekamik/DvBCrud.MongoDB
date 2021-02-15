using DvBCrud.MongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public interface ICRUDController<TEntity>
        where TEntity : BaseModel
    {
        bool IsActionAllowed(CRUDAction action);

        [HttpGet, Route("{id}")]
        ActionResult<TEntity> Read(string id);

        [HttpGet]
        ActionResult<IEnumerable<TEntity>> ReadAll();

        [HttpPost]
        IActionResult Create([FromBody] TEntity entity);
    }
}

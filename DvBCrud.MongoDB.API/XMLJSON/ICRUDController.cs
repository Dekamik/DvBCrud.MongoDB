using DvBCrud.MongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public interface ICRUDController<TModel>
        where TModel : BaseModel
    {
        bool IsActionAllowed(CRUDAction action);

        [HttpPost]
        IActionResult Create([FromBody] TModel data);

        [HttpGet, Route("{id}")]
        ActionResult<TModel> Read([FromQuery] string id);

        [HttpGet]
        ActionResult<IEnumerable<TModel>> ReadAll();

        [HttpPut, Route("{id}")]
        IActionResult Update([FromQuery] string id, [FromBody] TModel data);

        [HttpDelete, Route("{id}")]
        IActionResult Delete([FromQuery] string id);
    }
}

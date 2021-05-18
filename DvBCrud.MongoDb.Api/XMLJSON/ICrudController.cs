using System.Collections.Generic;
using DvBCrud.MongoDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public interface ICrudController<TModel>
        where TModel : BaseModel
    {
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

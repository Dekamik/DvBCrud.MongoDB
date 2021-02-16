using DvBCrud.MongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public interface IAsyncCRUDController<TModel>
        where TModel : BaseModel
    {
        [HttpPost]
        Task<IActionResult> Create([FromBody] TModel data);

        [HttpGet, Route("{id}")]
        Task<ActionResult<TModel>> Read([FromQuery] string id);

        [HttpGet]
        Task<ActionResult<IEnumerable<TModel>>> ReadAll();

        [HttpPut, Route("{id}")]
        Task<IActionResult> Update([FromQuery] string id, [FromBody] TModel data);

        [HttpDelete, Route("{id}")]
        Task<IActionResult> Delete([FromQuery] string id);
    }
}

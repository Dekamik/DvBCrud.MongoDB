using DvBCrud.MongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public interface ICRUDController<TEntity>
        where TEntity : BaseModel
    {
        bool IsActionAllowed(CRUDAction action);

        ActionResult<TEntity> Read(string id);

        ActionResult<IEnumerable<TEntity>> ReadAll();
    }
}

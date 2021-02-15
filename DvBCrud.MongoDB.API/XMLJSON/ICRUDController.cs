using DvBCrud.MongoDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public interface ICRUDController<TEntity>
        where TEntity : BaseModel
    {
        bool IsActionAllowed(CRUDAction action);

        ActionResult<TEntity> Read(string id);
    }
}

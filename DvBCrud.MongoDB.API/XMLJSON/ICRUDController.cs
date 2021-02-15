using DvBCrud.MongoDB.Models;

namespace DvBCrud.MongoDB.API.XMLJSON
{
    public interface ICRUDController<TEntity>
        where TEntity : BaseModel
    {
        bool IsActionAllowed(CRUDAction action);
    }
}

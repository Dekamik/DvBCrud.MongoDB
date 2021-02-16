using System.Collections.Generic;
using System.Linq;

namespace DvBCrud.MongoDB.API.CRUDActions
{
    public class CRUDActionPermissions
    {
        private readonly IEnumerable<CRUDAction> allowedActions;

        public CRUDActionPermissions()
        {

        }

        public CRUDActionPermissions(params CRUDAction[] allowedActions)
        {
            this.allowedActions = allowedActions;
        }

        public bool IsActionAllowed(CRUDAction action) => allowedActions?.Contains(action) ?? true;
    }
}

using DvBCrud.MongoDB.API.XMLJSON;
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

        public bool IsActionAllowed(CRUDAction action)
        {
            if (allowedActions == null)
            {
                return true;
            }

            return allowedActions.Contains(action);
        }
    }
}

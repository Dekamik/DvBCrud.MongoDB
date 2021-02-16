using DvBCrud.MongoDB.API.XMLJSON;
using System.Collections.Generic;
using System.Linq;

namespace DvBCrud.MongoDB.API.Actions
{
    public class ActionRestrictions
    {
        private readonly IEnumerable<CRUDAction> allowedActions;

        public ActionRestrictions()
        {

        }

        public ActionRestrictions(params CRUDAction[] allowedActions)
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

using System.Collections.Generic;
using System.Linq;

namespace DvBCrud.MongoDB.API.CRUDActions
{
    public class CrudActionPermissions
    {
        private readonly IEnumerable<CrudAction> _allowedActions;

        public CrudActionPermissions()
        {

        }

        public CrudActionPermissions(params CrudAction[] allowedActions)
        {
            _allowedActions = allowedActions;
        }

        public bool IsActionAllowed(CrudAction action) => _allowedActions?.Contains(action) ?? true;
    }
}

#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace DvBCrud.MongoDB.API.CrudActions
{
    public static class CrudActionExtensions
    {
        public static bool IsActionAllowed(this IEnumerable<CrudAction>? allowedActions, CrudAction action)
        {
            var allowedActionsArr = allowedActions?.ToArray();
            return allowedActionsArr == null ||
                   !allowedActionsArr.Any() ||
                   allowedActionsArr.Contains(action);
        }
    }
}
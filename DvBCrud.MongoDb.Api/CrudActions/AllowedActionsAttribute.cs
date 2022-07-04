using System;
using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.MongoDB.API.CrudActions
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Class)]
    public class AllowedActionsAttribute : Attribute
    {
        public CrudAction[] AllowedActions { get; }

        public AllowedActionsAttribute(params CrudAction[] allowedActions)
        {
            AllowedActions = allowedActions;
        }
    }
}



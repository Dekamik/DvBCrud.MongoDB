using System;
using System.Diagnostics.CodeAnalysis;
using DvBCrud.MongoDB.API.CrudActions;

namespace DvBCrud.MongoDB.API.Swagger
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerDocsFilterAttribute : Attribute
    {
        public CrudAction HideIfNotAllowed { get; }
    
        public SwaggerDocsFilterAttribute(CrudAction hideIfNotAllowed)
        {
            HideIfNotAllowed = hideIfNotAllowed;
        }
    }
}

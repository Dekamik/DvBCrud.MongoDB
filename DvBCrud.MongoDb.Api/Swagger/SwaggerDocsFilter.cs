using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using DvBCrud.MongoDB.API.CrudActions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DvBCrud.MongoDB.API.Swagger
{
    [ExcludeFromCodeCoverage]
    public class SwaggerDocsFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var description in context.ApiDescriptions)
            {
                var actionDescriptor = (ControllerActionDescriptor)description.ActionDescriptor;
                var action = actionDescriptor.MethodInfo.GetCustomAttribute<SwaggerDocsFilterAttribute>()?.HideIfNotAllowed;
                var allowedActionsAttribute = actionDescriptor.ControllerTypeInfo.GetCustomAttribute<AllowedActionsAttribute>();
                
                if (action == null || allowedActionsAttribute == null)
                    continue;

                if (allowedActionsAttribute.AllowedActions.IsActionAllowed(action.Value)) 
                    continue;
                
                var method = description.HttpMethod;
                var controllerName = actionDescriptor.ControllerName;

                foreach (var path in swaggerDoc.Paths)
                {
                    if (!IsGeneratedEndpoint(controllerName, path.Key))
                        continue;
                    
                    foreach (var operation in path.Value.Operations.Keys)
                    {
                        switch (operation)
                        {
                            case OperationType.Get:
                                if (method == "GET")
                                    path.Value.Operations.Remove(operation);
                                break;
                            
                            case OperationType.Post:
                                if (method == "POST")
                                    path.Value.Operations.Remove(operation);
                                break;
                            
                            case OperationType.Put:
                                if (method == "PUT")
                                    path.Value.Operations.Remove(operation);
                                break;
                            
                            case OperationType.Delete:
                                if (method == "DELETE")
                                    path.Value.Operations.Remove(operation);
                                break;

                            case OperationType.Options:
                            case OperationType.Head:
                            case OperationType.Patch:
                            case OperationType.Trace:
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private static bool IsGeneratedEndpoint(string controllerName, string path) =>
            path == "/" + controllerName || path == "/" + controllerName + "/{id}";
    }
}

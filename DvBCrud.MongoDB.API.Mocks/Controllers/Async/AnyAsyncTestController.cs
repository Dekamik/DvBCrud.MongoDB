﻿using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.API.XMLJSON;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Async
{
    public class AnyAsyncTestController : AsyncCRUDController<AnyModel, IAnyRepository>, IAnyAsyncTestController
    {
        public AnyAsyncTestController(IAnyRepository repository, ILogger logger, params CRUDAction[] allowedActions) : base(repository, logger, allowedActions)
        {
        }
    }
}

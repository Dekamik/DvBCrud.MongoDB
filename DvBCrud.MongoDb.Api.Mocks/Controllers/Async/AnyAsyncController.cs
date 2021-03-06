﻿using DvBCrud.MongoDB.API.XMLJSON;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Async
{
    public class AnyAsyncController : AsyncCrudController<AnyModel, IAnyRepository>, IAnyAsyncController
    {
        public AnyAsyncController(IAnyRepository repository, ILogger logger) : base(repository, logger)
        {
        }
    }
}

﻿using DvBCrud.MongoDB.API.CRUDActions;
using DvBCrud.MongoDB.API.Controllers;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Mocks.Controllers.Async
{
    public class AnyAsyncReadOnlyController : AsyncCrudController<AnyModel, IAnyRepository>
    {
        public AnyAsyncReadOnlyController(IAnyRepository repository) : base(repository, CrudAction.Read)
        {
        }
    }
}

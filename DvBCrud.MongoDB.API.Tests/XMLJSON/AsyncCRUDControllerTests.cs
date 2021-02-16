using DvBCrud.MongoDB.API.Mocks.Controllers;
using DvBCrud.MongoDB.Mocks.Repositories;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace DvBCrud.MongoDB.API.Tests.XMLJSON
{
    public class AsyncCRUDControllerTests
    {
        private readonly IAnyRepository repository;
        private readonly ILogger logger;
        private readonly IAnyAsyncController controller;

        public AsyncCRUDControllerTests()
        {
            repository = A.Fake<IAnyRepository>();
            logger = A.Fake<ILogger>();
            controller = new AnyAsyncController(repository, logger);
        }
    }
}

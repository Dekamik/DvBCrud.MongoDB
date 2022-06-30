﻿using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Repositories;
using DvBCrud.MongoDB.Repositories.Wrappers;
using MongoDB.Driver;

namespace DvBCrud.MongoDB.Mocks.Repositories
{
    public class AnyRepository : Repository<AnyModel>, IAnyRepository
    {
        public AnyRepository(IMongoClient client, IMongoCollectionWrapperFactory factory, string databaseName) : base(client, factory, databaseName)
        {

        }
    }
}

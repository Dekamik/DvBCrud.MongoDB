using System;
using MongoDB.Driver;

namespace DvBCrud.MongoDb.Api.TestApiOld
{
    public class MongoClientFactory
    {
        public static MongoClient Create()
        {
            var username = Environment.GetEnvironmentVariable(EnvironmentVariables.DbUsername) 
                           ?? throw new ArgumentException($"{EnvironmentVariables.DbUsername} must be defined.");
            var password = Environment.GetEnvironmentVariable(EnvironmentVariables.DbPassword) 
                           ?? throw new ArgumentException($"{EnvironmentVariables.DbPassword} must be defined.");
            var server = Environment.GetEnvironmentVariable(EnvironmentVariables.DbServer) 
                         ?? throw new ArgumentException($"{EnvironmentVariables.DbServer} must be defined.");
            var port = Environment.GetEnvironmentVariable(EnvironmentVariables.DbPort) 
                       ?? throw new ArgumentException($"{EnvironmentVariables.DbPort} must be defined.");
            var database = Environment.GetEnvironmentVariable(EnvironmentVariables.DbDatabase) 
                           ?? throw new ArgumentException($"{EnvironmentVariables.DbDatabase} must be defined.");

            var connectionString =
                $"mongodb://{username}:{Uri.EscapeDataString(password)}@{server}:{port}/{database}?retryWrites=true&w=majority";

            return new MongoClient(connectionString);
        }
    }
}
using System.Diagnostics.CodeAnalysis;
using MongoDB.Driver;

namespace DvBCrud.MongoDb.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class MongoClientFactory
    {
        /// <summary>
        /// Helper method for creating a new MongoClient and extracting the database name from the connection string.
        /// </summary>
        /// <param name="connectionString">The MongoDB connection string.</param>
        /// <returns>A tuple containing the MongoClient and the database name as a string</returns>
        public static (MongoClient, string) Create(string connectionString)
        {
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            var client = new MongoClient(connectionString);
            return (client, databaseName);
        }
    }
}
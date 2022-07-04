namespace DvBCrud.MongoDb.Api.TestApi
{
    public static class EnvironmentVariables
    {
        public static readonly string? DbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }
}
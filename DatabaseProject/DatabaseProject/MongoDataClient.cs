using MongoDB.Driver;

namespace DatabaseProject
{
    public static class MongoDataClient
    {
        private const string _database = "DealershipDatabase";
        private const string _mongoConnectionString = "mongodb://localhost:27017";

        public static IMongoDatabase GetDealershipDatabase()
        {
            var client = new MongoClient(_mongoConnectionString);
            return client.GetDatabase(_database);
        }
    }
}

using MongoDB.Bson;
using MongoDB.Driver;

namespace simpleAPI
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDB:ConnectionString"];
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        }

        public IMongoCollection<BsonDocument> GetCollection(string collectionName) =>
            _database.GetCollection<BsonDocument>(collectionName);
    }
}

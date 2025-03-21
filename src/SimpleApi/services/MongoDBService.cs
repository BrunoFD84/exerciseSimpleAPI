using MongoDB.Bson;
using MongoDB.Driver;
using StackExchange.Redis;

namespace simpleAPI.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly IConnectionMultiplexer _redisConnection;

        public MongoService(IConnectionMultiplexer redisConnection, MongoDBContext dbContext)
        {
            _redisConnection = redisConnection;
            _collection = dbContext.GetCollection("JsonStorage");
        }

        public async Task<string> InsertJsonAsync(string json)
        {
            var document = BsonDocument.Parse(json);
            await _collection.InsertOneAsync(document);

            string value = DateTime.UtcNow.ToString("o"); // Timestamp em formato ISO 8601

            string key = document["_id"].ToString();

            var redisDb = _redisConnection.GetDatabase();
            redisDb.StringSet(key, value);

            return key;
        }


        public async Task<BsonDocument> GetJsonAsync(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<List<BsonDocument>> getAll()
        {
            return await _collection.Find(FilterDefinition<BsonDocument>.Empty).ToListAsync();
        }
    }
}

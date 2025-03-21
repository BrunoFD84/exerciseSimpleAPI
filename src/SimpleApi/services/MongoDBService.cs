using MongoDB.Bson;
using MongoDB.Driver;

namespace simpleAPI.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public MongoService(MongoDBContext dbContext)
        {
            _collection = dbContext.GetCollection("JsonStorage");
        }

        public async Task<string> InsertJsonAsync(string json)
        {
            var document = BsonDocument.Parse(json);
            await _collection.InsertOneAsync(document);

            return document["_id"].ToString();
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

using MongoDB.Bson;
using MongoDB.Driver;

namespace simpleAPI.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public MongoService(MongoDBContext dbContext)
        {
            //salvo erro se não existir é criado?
            _collection = dbContext.GetCollection("JsonStorage");
        }

        public async Task<string> InsertJsonAsync(string json)
        {
            var document = BsonDocument.Parse(json);
            await _collection.InsertOneAsync(document);

            return document["_id"].ToString();
        }
    }
}

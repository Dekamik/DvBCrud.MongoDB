using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DvBCrud.MongoDB.Models
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}

using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DvBCrud.MongoDB.Models
{
    [ExcludeFromCodeCoverage]
    public class BaseDataModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}

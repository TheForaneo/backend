using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webapi.Models{
    public class Comentario{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id{get; private set;}
        public string IdTaller{get; set;}
        [BsonElement("Comentario")]
        public string comentairo{get; set;}
        public string IdCliente{get; set;}
    }
}
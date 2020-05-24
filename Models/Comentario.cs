using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webapi.Models{
    public class Comentario{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id{get; private set;}
        public string idTaller{get; set;}
        [BsonElement("Comentario")]
        public string comentario{get; set;}
        public string idCliente{get; set;}
    }
}
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace webapi.Models{
    public class Solicitud{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id{get; set;}
        public string claveCliente{get;set;}
        public string placa{get;set;}
        public string descripcionProblema{get;set;}
        public string tiempoProblema{get;set;}
        public string tallerId{get;set;}
    }
}
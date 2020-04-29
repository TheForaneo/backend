using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace webapi.Models{
    public class Solicitud{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id{get; set;}
        public string claveCliente{get;set;}
        public string placa{get;set;}
        public string descripcionProblema{get;set;}
        public string tiempoProblema{get;set;}

        [BsonElement("fechaRealizacion")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime creacionSolicitid{get; set;}

        [BsonElement("fechaProgramada")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime entrada{get; set;}

        [BsonElement("fechaSalida")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Salida{get; set;}
        public string tallerId{get;set;}
    }
}
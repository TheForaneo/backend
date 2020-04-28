using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace webapi.Models{
    public class Taller{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id{get; set;}
        public string nombreTaller{get; set;}
        public string correo{get;set;}
        public string telefonoFijo{get;set;}
        public string celular{get;set;}
        public string nombreDueño{get;set;}
        public string calle{get;set;}
        public string numExt{get;set;}
        public string numInt{get;set;}
        public string colonia{get;set;}
        public string contraseña{private get;set;}
    }
}
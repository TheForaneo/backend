using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace webapi.Models{
    public class Cliente{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; private set; }
        
        [BsonElement ("apellidop")]
        public string apellidop { get; set; }

        [BsonElement ("apellidom")]
        public string apellidom { get; set; } 

        [BsonElement ("nombre")]  
        public string Nombre { get; set; }  

        [BsonElement ("calle")]
        public string calle { get; set; }

        [BsonElement ("numCasa")]
        public string numCasa { get; set; }
    
        [BsonElement ("colonia")]
        public string colonia { get; set; }

        [BsonElement ("celular")]
        public string celular { get; set; }
    
        [BsonElement ("correo")]
        public string correo { get; set; }

        [BsonElement("rol")]
        public string rol{get; set;}

        [BsonElement ("contraseña")]
        public string contraseña { get; set; }

        [BsonElement("codigo")]
        public string codigo{get; set;}
    }
}
using System.Collections.Generic;
using MongoDB.Driver;
using webapi.Models;

namespace webapi.Services{
    public class MensajeriaService{
        private readonly IMongoCollection<Mensajeria> _mensajeria;

        public MensajeriaService(IMensajeriastoreDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _mensajeria = database.GetCollection<Mensajeria>(settings.MensajeriaCollectionName);
        }

        public List<Mensajeria> GetMensajesRecibidosCliente(string idCliente) => _mensajeria.Find<Mensajeria>(mensajeria => mensajeria.Clienteid == idCliente).Sort("FechaEnvio").ToList();

        public List<Mensajeria> GetMensajesRecibidosTaller(string idTaller) => _mensajeria.Find<Mensajeria>(mensajeria => mensajeria.Tallerid == idTaller).ToList();

        public Mensajeria Create(Mensajeria mensaje){
            _mensajeria.InsertOne(mensaje);
            return mensaje;
        }
    }
}
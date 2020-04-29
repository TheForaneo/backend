using webapi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;

namespace webapi.Services{
    public class SolicitudService{
        private readonly IMongoCollection<Solicitud> _solicitud;

        public SolicitudService(ISolicitudstoreDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _solicitud = database.GetCollection<Solicitud>(settings.SolicitudCollectionName);
        }

        public List<Solicitud> Get() => _solicitud.Find<Solicitud>(solicitud => true).ToList();

        public Solicitud Get(string id) => _solicitud.Find<Solicitud>(solicitud => solicitud.Id == id).FirstOrDefault();

        public Solicitud GetV(string placa) => _solicitud.Find<Solicitud>(solicitud => solicitud.placa.Equals(placa)).FirstOrDefault();

        public Solicitud Create(Solicitud solicitud){
            _solicitud.InsertOne(solicitud);
            return solicitud;
        } 
        public int checkFecha(string placa, DateTime fecEntrada){
            var val = GetV(placa);
            int fecha1 = val.entrada.Hour;
            int fecha2 = fecEntrada.Hour;
            int dif = (fecha2 - fecha1);
            return dif;
        }
        public void Update(string id, Solicitud solicitudIn) => _solicitud.ReplaceOne(solicitud => solicitud.Id == id, solicitudIn);

        public void Remove(Solicitud solicitudIn) => _solicitud.DeleteOne(solicitud => solicitud.Id== solicitudIn.Id);

        public void Remove(string id) => _solicitud.DeleteOne(solicitud => solicitud.Id == id);
    }

}
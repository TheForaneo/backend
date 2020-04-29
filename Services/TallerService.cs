using webapi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;

namespace webapi.Services{
    public class TallerService{
        private readonly IMongoCollection<Taller> _taller;
        
        public TallerService (ITallerstoreDatabaseSettings settings){
            var taller = new MongoClient(settings.ConnectionString);
            var database = taller.GetDatabase(settings.DatabaseName);

            _taller = database.GetCollection<Taller>(settings.TallerCollectionName);
        }

        public List<Taller> Get() => _taller.Find<Taller>(taller => true).ToList();

        public Taller Get(string id) => _taller.Find<Taller>(taller => taller.Id == id).FirstOrDefault();

        public List<Taller> GetCor(string correo) => _taller.Find<Taller>(taller => taller.correo.Equals(correo)).ToList();

        public List<Taller> GetCel(string celular) => _taller.Find<Taller>(taller => taller.celular.Equals(celular)).ToList();

        public Taller Create(Taller taller){
            _taller.InsertOne(taller);
            return taller;
        }

        public Boolean checkCorreo(string correo){
            int cont = GetCor(correo).Count();
            if(cont >= 1){
                return true;
            }
            return false;
        }
        public Boolean checkCelular(string celular){
            int cont = GetCel(celular).Count();
            if(cont >= 1){
                return true;
            }
            return false;
        }

        public void Update(string id, Taller tallerIn) => _taller.ReplaceOne(taller => taller.Id == tallerIn.Id, tallerIn);
        public void Remove(Taller tallerIn) => _taller.DeleteOne(taller => taller.Id== tallerIn.Id);

        public void Remove(string id) => _taller.DeleteOne(taller => taller.Id == id);

    }
}
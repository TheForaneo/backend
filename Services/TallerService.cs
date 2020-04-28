using webapi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

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

        public Taller Create(Taller taller){
            _taller.InsertOne(taller);
            return taller;
        }
        public void Update(string id, Taller tallerIn) => _taller.ReplaceOne(taller => taller.Id == tallerIn.Id, tallerIn);
        public void Remove(Taller tallerIn) => _taller.DeleteOne(taller => taller.Id== tallerIn.Id);

        public void Remove(string id) => _taller.DeleteOne(taller => taller.Id == id);

    }
}
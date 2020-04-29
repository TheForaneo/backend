using webapi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace webapi.Services{

    public class VehiculoService{
        private readonly IMongoCollection<Vehiculo> _vehiculo;
        
        public VehiculoService(IVehiculostoreDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _vehiculo=database.GetCollection<Vehiculo>(settings.VehiculoCollectionName);
        }

        public List<Vehiculo> Get() => _vehiculo.Find<Vehiculo>(vehiculo => true).ToList();

        public Vehiculo Get(string id) => _vehiculo.Find<Vehiculo>(vehiculo => vehiculo.Id == id).FirstOrDefault();

        public List<Vehiculo> GetV(string placa) => _vehiculo.Find<Vehiculo>(vehiculo => vehiculo.placa.Equals(placa)).ToList();

        public Vehiculo Create(Vehiculo vehiculo){
            _vehiculo.InsertOne(vehiculo);
            return vehiculo;
        }

        public int checkV(string placa) {
            var cont = GetV(placa).Count;
            return cont;
        }
        
        public void Update(string id, Vehiculo vehiculoIn) => _vehiculo.ReplaceOne(vehiculo => vehiculo.Id == vehiculoIn.Id, vehiculoIn);

        public void Remove(Vehiculo vehiculoIn) => _vehiculo.DeleteOne(vehiculo => vehiculo.Id == vehiculoIn.Id);

        public void Remove(string id) => _vehiculo.DeleteOne(vehiculo=> vehiculo.Id==id);
    }
    


}

using webapi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace webapi.Services{
    public class ClienteService{
        private readonly IMongoCollection<Cliente> _cliente;

        public ClienteService(IClientestoreDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _cliente = database.GetCollection<Cliente>(settings.ClienteCollectionName);
        }

        public List<Cliente> Get() => _cliente.Find<Cliente>(cliente => true).ToList();

        public Cliente Get(string id) => _cliente.Find<Cliente>(cliente => cliente.Id == id).FirstOrDefault();

        public Cliente Create(Cliente cliente){
            _cliente.InsertOne(cliente);
            return cliente;
        } 
        public void Update(string id, Cliente clienteIn) => _cliente.ReplaceOne(cliente => cliente.Id == id, clienteIn);

        public void Remove(Cliente clienteIn) => _cliente.DeleteOne(cliente => cliente.Id== clienteIn.Id);

        public void Remove(string id) => _cliente.DeleteOne(cliente => cliente.Id == id);
    }
}
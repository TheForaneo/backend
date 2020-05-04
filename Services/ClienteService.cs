using webapi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public Cliente GetCorreo(string corro) => _cliente.Find<Cliente>(cliente => cliente.correo == corro).FirstOrDefault();

        public List<Cliente> GetC(string correo) => _cliente.Find<Cliente>(cliente => cliente.correo.Equals(correo)).ToList();

        public List<Cliente> GetCel(string celular) => _cliente.Find<Cliente>(cliente => cliente.celular.Equals(celular)).ToList();

        public Cliente Create(Cliente cliente){
            _cliente.InsertOne(cliente);
            return cliente;
        } 
        public Cliente iniciaSesion(UserLogin cli){
            var cliente = GetCorreo(cli.Email);
            if(cliente != null){
                if(cliente.contraseña.Equals(cli.Password)){
                    return cliente;
                }
            }
            return null;
        }
        public Boolean checkCorreo(string correo){
            int cont = GetC(correo).Count();
            if(cont >= 1){
                return true;
            }
            return false;
        }
        public Boolean checkCelular(string celular){
            int cont = GetC(celular).Count();
            if(cont >= 1){
                return true;
            }
            return false;
        }
        public void Update(string id, Cliente clienteIn) => _cliente.FindOneAndUpdate(cliente=>cliente.Id==id, Builders<Cliente>.Update.Set("apellidop", clienteIn.apellidop).Set("apellidom",clienteIn.apellidom).Set("nombre",clienteIn.Nombre).Set("calle",clienteIn.calle)
        .Set("numCasa",clienteIn.numCasa).Set("colonia",clienteIn.colonia).Set("celular", clienteIn.celular).Set("correo",clienteIn.correo));

        public void Remove(Cliente clienteIn) => _cliente.DeleteOne(cliente => cliente.Id== clienteIn.Id);

        public void Remove(string id) => _cliente.DeleteOne(cliente => cliente.Id == id);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;  
using MongoDB.Bson;  
using webapi.Models;

namespace webapi.Controllers{

    [ApiController]
    public class ClienteControllers:Controller{
        [Route("agregarCliente")]
        [HttpPost]
        public Boolean agregar(Cliente obj){
            try{
                var Client= new MongoClient("mongodb://localhost:27017");
                var db= Client.GetDatabase("Cliente");
                var collection = db.GetCollection<Cliente>("Cliente");
                collection.InsertOne(obj);
                return true;
            }
            catch(Exception ex){
                return false;
            }
        }
        [Route ("modificarCliente")]
        [HttpPost]
        public Boolean modificarCliente(Cliente objC){
            try{
                var Client= new MongoClient("mongodb://localhost:27017");
                var db= Client.GetDatabase("Cliente");
                var collection = db.GetCollection<Cliente>("Cliente");
                var update = collection.FindOneAndUpdateAsync(Builders<Cliente>.Filter.Eq("Id", objC.Id), 
                Builders<Cliente>.Update.Set("apellidop", objC.apellidop).Set("apellidom", objC.apellidop).Set("nombre", objC.Nombre).Set("calle", objC.calle).Set("numCasa", objC.numCasa).Set("colonia", objC.colonia).Set("celular",objC.celular
                ).Set("correo", objC.correo));  
                return true;
            }
            catch(Exception ex){
                return false;
            }
            
        }

    }
    
}
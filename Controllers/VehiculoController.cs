using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;  
using MongoDB.Bson;  
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    public class VehiculoController : Controller{
        [Route("agregarVehiculo")]
        [HttpPost]
        public Boolean addVehiculo(Vehiculo objV){
            try{
                var Client= new MongoClient("mongodb://localhost:27017");
                var db = Client.GetDatabase("Proyecto");
                var collection = db.GetCollection<Vehiculo>("Vehiculo");
                collection.InsertOne(objV);
                return true;
            }
            catch(Exception ex){
                return false;
            }
             
        }

        [Route("modificarVehiculo")]
        [HttpPost]
        public Boolean updateVehiculo(Vehiculo objV){
            try{
                var Client= new MongoClient("mongodb://localhost:27017");
                var db = Client.GetDatabase("Proyecto");
                var collection = db.GetCollection<Vehiculo>("Vehiculo");
                var updater = collection.FindOneAndUpdateAsync(Builders<Vehiculo>.Filter.Eq("Id",objV.Id), 
                Builders<Vehiculo>.Update.Set("modelo", objV.modelo).Set("año", objV.año).Set("marca", objV.marca));
                return true;
            }
            catch(Exception ex){
                return false;

            }

        }

        [Route ("mostrarTodos")]
        [HttpGet]
        public object mostarTodos(){
            var Client= new MongoClient("mongodb://localhost:27017");
            var db= Client.GetDatabase("Proyecto");
            var collection = db.GetCollection<Vehiculo>("Vehiculo");
            return Json(collection);
        }

        [Route ("borrarVehiculo")]
        [HttpGet]
        public Boolean borrarVehiculo(string id){
            try{
                var Client= new MongoClient("mongodb://localhost:27017");
                var db = Client.GetDatabase("Proyecto");
                var collection = db.GetCollection<Vehiculo>("Vehiculo");

                var borrarDato = collection.DeleteOneAsync(Builders<Vehiculo>.Filter.Eq("Id", id));
                return true;
            }
            catch(Exception ex){
                return false;
            }
             

             

        }
    }
}
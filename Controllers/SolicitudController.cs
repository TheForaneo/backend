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
    public class SolicitudController : Controller{
        [Route("crearSolicitud")]
        [HttpPost]
        public Boolean crearSolicitud(Solicitud objS){
            try{
                var Client= new MongoClient("mongodb://localhost:27017");
                var db = Client.GetDatabase("Proyecto");
                var collection = db.GetCollection<Solicitud>("Solicitud");
                collection.InsertOne(objS);
                return true;
            }
            catch(Exception ex){
                return false;
            }
        }
        [Route("mostrarSolicitudes")]
        [HttpGet]
        public object mostrarSolicitudes(){
            var Client= new MongoClient("mongodb://localhost:27017");
            var db= Client.GetDatabase("Proyecto");
            var collection = db.GetCollection<Solicitud>("Solicitud");
            return Json(collection);
        }
        [Route("modificarSolicitud")]
        [HttpPost]
        public Boolean modificarSolicitud(Solicitud objS){
            try{
                var Client= new MongoClient("mongodb://localhost:27017");
                var db = Client.GetDatabase("Proyecto");
                var collection = db.GetCollection<Solicitud>("Solicitud");
                var updater = collection.FindOneAndUpdateAsync(Builders<Solicitud>.Filter.Eq("Id",objS.Id), 
                Builders<Solicitud>.Update.Set("descripcionProblema", objS.descripcionProblema).Set("tiempoProblema",objS.tiempoProblema));
                return true;
            }
            catch(Exception ex){
                return false;
            }
        }
        [Route ("cancelarSolicitud")]
        [HttpGet]
        public Boolean cancelarSolicitud(string id){
            try{
                var Client= new MongoClient("mongodb://localhost:27017");
                var db = Client.GetDatabase("Proyecto");
                var collection = db.GetCollection<Solicitud>("Solicitud");

                var borrarDato = collection.DeleteOneAsync(Builders<Solicitud>.Filter.Eq("Id", id));
                return true;
            }
            catch(Exception ex){
                return false;
            }

        }

    }
}
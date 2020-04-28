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

    public class TallerController : Controller{
        [Route("RegistarTaller")]
        [HttpPost]
        public Boolean RegistarTaller(Taller objT){
            try{
                var Client= new MongoClient("mongodb://localhost:27017");
                var db = Client.GetDatabase("Proyecto");
                var collection = db.GetCollection<Taller>("Taller");
                collection.InsertOne(objT);
                return true;
            }
            catch( Exception ex){
                return false;
            }
        }
        [Route("modificarTaller")]
        [HttpPost]
        public Boolean modificarTaller(Taller objT){
            try{
                var Client= new MongoClient("mongodb://localhost:27017");
                var db = Client.GetDatabase("Proyecto");
                var collection = db.GetCollection<Taller>("Taller");
                var updater = collection.FindOneAndUpdateAsync(Builders<Taller>.Filter.Eq("Id",objT.Id), 
                Builders<Taller>.Update.Set("nombreTaller",objT.nombreTaller).Set("correo",objT.correo).Set("telefonoFijo",objT.telefonoFijo).Set("celular",objT.celular).Set("nombreDueño",objT.nombreDueño).Set("calle",objT.calle).Set("numExt",objT.numExt).Set("numInt",objT.numInt).Set("colonia",objT.colonia));
                return true;
            }
            catch(Exception ex){
                return false;
            }
        }
    }
}
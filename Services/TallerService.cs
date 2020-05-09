using webapi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;

namespace webapi.Services{
    public class TallerService{
        private readonly IMongoCollection<Taller> _taller;

        private readonly IMongoCollection<Solicitud> _solicitud;
        
        public TallerService (ITallerstoreDatabaseSettings settings, ISolicitudstoreDatabaseSettings settingsS){
            var taller = new MongoClient(settings.ConnectionString);
            var database = taller.GetDatabase(settings.DatabaseName);

            _taller = database.GetCollection<Taller>(settings.TallerCollectionName);
            _solicitud = database.GetCollection<Solicitud>(settingsS.SolicitudCollectionName);
        }

        public List<Taller> Get() => _taller.Find<Taller>(taller => true).ToList();

        public Taller Get(string id) => _taller.Find<Taller>(taller => taller.Id == id).FirstOrDefault();

        public Taller GetCorreo(string correo) => _taller.Find<Taller>(taller => taller.correo.Equals(correo)).FirstOrDefault();

         public Taller GetCelular(string celular) => _taller.Find<Taller>(taller => taller.celular == celular).FirstOrDefault();

        public List<Taller> GetCor(string correo) => _taller.Find<Taller>(taller => taller.correo.Equals(correo)).ToList();

        public List<Taller> GetCel(string celular) => _taller.Find<Taller>(taller => taller.celular.Equals(celular)).ToList();

        public List<Solicitud> GetCitas(string id) => _solicitud.Find<Solicitud>(solicitud => solicitud.tallerId == id).ToList();

        public Taller Create(Taller taller){
            _taller.InsertOne(taller);
            return taller;
        }
        public Boolean insertCodigo(Taller tal, string codigo){
            var user = GetCorreo(tal.correo);
            if(user!=null){
                if(!(codigo.Equals(null))){
                    _taller.FindOneAndUpdate(taller => taller.correo.Equals(tal.correo), Builders<Taller>.Update.Set("codigo", codigo));
                    return true;
                }
            }
            return false;
        }
        public Boolean correoExist(string correo){
            int cont = GetCor(correo).Count();
            if(cont >= 1){
                return true;
            }
            return false;
        }
        public Boolean celularExist(string celular){
            int cont = GetCel(celular).Count();
            if(cont >= 1){
                return true;
            }
            return false;
        }
        public Taller iniciaSesionEmail(UserEmailLogin model){
            var taller = GetCorreo(model.Email);
            if(taller != null){
                if(taller.contraseña.Equals(model.Password)){
                    return taller;
                }
            }
            return null;
        }
        public Taller iniciaSesionCell(UserCellLogin model){
            var taller = GetCelular(model.Cellphone);
            if(taller != null){
                if(taller.contraseña.Equals(model.Password)){
                    return taller;
                }
            }
            return null;
        }
        //public void Update(string id, Taller tallerIn) => _taller.ReplaceOne(taller => taller.Id == tallerIn.Id, tallerIn);
        
        public void Update(string id, Taller tallerIn){
            if(tallerIn != null){
                try{
                    if(!(tallerIn.nombreTaller.Equals(null))){
                        _taller.FindOneAndUpdate(taller => taller.Id == id, Builders<Taller>.Update.Set("nombreTaller", tallerIn.nombreTaller));
                    }    
                }catch(NullReferenceException ex){}
                try{
                    if(!(tallerIn.correo.Equals(null))){
                        _taller.FindOneAndUpdate(taller => taller.Id == id, Builders<Taller>.Update.Set("correo", tallerIn.correo));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(tallerIn.telefonoFijo.Equals(null))){
                        _taller.FindOneAndUpdate(taller => taller.Id == id, Builders<Taller>.Update.Set("telefonoFijo", tallerIn.telefonoFijo));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(tallerIn.celular.Equals(null))){
                        _taller.FindOneAndUpdate(taller => taller.Id == id, Builders<Taller>.Update.Set("celular", tallerIn.celular));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(tallerIn.nombreDueño.Equals(null))){
                        _taller.FindOneAndUpdate(taller => taller.Id == id, Builders<Taller>.Update.Set("nombreDueño", tallerIn.nombreDueño));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(tallerIn.calle.Equals(null))){
                        _taller.FindOneAndUpdate(taller => taller.Id == id, Builders<Taller>.Update.Set("calle", tallerIn.calle));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(tallerIn.numExt.Equals(null))){
                        _taller.FindOneAndUpdate(taller => taller.Id==id, Builders<Taller>.Update.Set("numExt",tallerIn.numExt));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(tallerIn.numInt.Equals(null))){
                        _taller.FindOneAndUpdate(taller => taller.Id == id, Builders<Taller>.Update.Set("numInt",tallerIn.numInt));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(tallerIn.colonia.Equals(null))){
                        _taller.FindOneAndUpdate(taller => taller.Id == id, Builders<Taller>.Update.Set("colonia", tallerIn.colonia));
                    }
                }catch(NullReferenceException ex){}
            }
        }
        public void Remove(Taller tallerIn) => _taller.DeleteOne(taller => taller.Id== tallerIn.Id);
        public void Remove(string id) => _taller.DeleteOne(taller => taller.Id == id);

    }
}
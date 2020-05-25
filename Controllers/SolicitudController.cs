using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;  
using MongoDB.Bson;  
using webapi.Models;
using webapi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace webapi.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SolicitudController : Controller{
        private readonly SolicitudService _solicitudService;
        private readonly VehiculoService _vehiculoService;
        private readonly TallerService _tallerService;

        DateTime date = new DateTime();

        public SolicitudController(SolicitudService solicitudService, VehiculoService vehiculoService, TallerService tallerService){
            _solicitudService = solicitudService;
            _tallerService =tallerService;
            _vehiculoService =vehiculoService;
        }
        
        [HttpGet]
        public ActionResult<List<Solicitud>> Get() => _solicitudService.Get();
        
        [HttpGet("porCliente/{clienteid:length(24)}", Name="SolicitudesByCliente")]
        public ActionResult<List<Solicitud>> SolicitudesByCliente(string clienteid){ 
            if(_solicitudService.GetSolicitudesByCliente(clienteid).Count >= 1){
                List<Solicitud> lista = _solicitudService.GetSolicitudesByCliente(clienteid);
                Taller taller;
                Vehiculo veh;
                for(int i=0; i<lista.Count; i++){
                    var sol = lista.ElementAt(i);
                    taller = _tallerService.Get(sol.tallerId);
                    veh = _vehiculoService.GetV(sol.placa);
                    sol.nombreTaller= taller.nombreTaller;
                    sol.modeloVehiculo = veh.modelo;
                }
                return lista;
            }
            return NotFound();
        } 

        [HttpGet("porTaller/{tallerid:length(24)}", Name="SolicitudesByTaller")]
        public ActionResult<List<Solicitud>> SolicitudesByTaller(string tallerid){
            if(_tallerService.GetCitas(tallerid).Count >= 1){
                List<Solicitud> lista = _tallerService.GetCitas(tallerid);
                Taller taller;
                Vehiculo veh;
                for(int i=0; i<lista.Count; i++){
                    var sol = lista.ElementAt(i);
                    taller = _tallerService.Get(sol.tallerId);
                    veh = _vehiculoService.GetV(sol.placa);
                    sol.nombreTaller= taller.nombreTaller;
                    sol.modeloVehiculo = veh.modelo;
                }
                return lista;
            }
            return NotFound();
        }

        [HttpGet("getSolicitud/{solicitudid:length(24)}", Name="GetSolicitud")]
        public ActionResult GetSolicitud(string solicitudid){
            var solicitud = _solicitudService.GetS(solicitudid);
            if(solicitud==null){
                return NotFound();
            }
            Vehiculo vehiculo = _vehiculoService.GetV(solicitud.placa);
            Taller taller = _tallerService.Get(solicitud.tallerId);
            return Ok((new {solicitud, vehiculo, taller}));
        }

        [HttpPost]
        public ActionResult<Solicitud> Create(Solicitud solicitud){
            var vehiculo =_solicitudService.GetV(solicitud.placa);
            if(vehiculo != null){
                return NoContent();
            }
            DateTime dateOnly = DateTime.Now;
            var date1=dateOnly.Date;
            solicitud.creacionSolicitid=date1.ToString("d");
            _solicitudService.Create(solicitud);

            return CreatedAtRoute("GetSolicitud", new {solicitudid = solicitud.Id.ToString()}, solicitud);
        }
        [HttpPut]
        public IActionResult Update(string id, Solicitud solicitudIn){
            var solicitud = _solicitudService.GetS(id);
            if(solicitud==null){
                return NotFound();
            }
            _solicitudService.Update(id, solicitudIn);
            solicitud =_solicitudService.GetS(id);
            return Ok(solicitud);
        }
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id){
            var solicitud = _solicitudService.GetS(id);
            if(solicitud==null){
                return NotFound();
            }
            _solicitudService.Remove(solicitud.Id);
            return Ok();
        }
    }
}
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

namespace webapi.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SolicitudController : Controller{
        private readonly SolicitudService _solicitudService;
        private readonly VehiculoService _vehiculoService;
        private readonly TallerService _tallerService;

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
                return _solicitudService.GetSolicitudesByCliente(clienteid);
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
            if(solicitud==null/*_solicitudService.checkFecha(solicitud.placa, solicitud.entrada)<=2*/){
                return NoContent();
            }
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
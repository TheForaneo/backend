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
    [Authorize]
    public class SolicitudController : Controller{
        private readonly SolicitudService _solicitudService;

        public SolicitudController(SolicitudService solicitudService){
            _solicitudService = solicitudService;
        }
        
        [HttpGet]
        public ActionResult<List<Solicitud>> Get() => _solicitudService.Get();

        [HttpGet("{id:length(24)}", Name="SolicitudesByCliente")]
        [Route("[action]")]
        public ActionResult<List<Solicitud>> SolicitudesByCliente(string clienteid){ 
            if(_solicitudService.GetSolicitudesByCliente(clienteid).Count >= 1){
                return _solicitudService.GetSolicitudesByCliente(clienteid);
            }
            return NotFound();
        } 

        [HttpGet("{id:length(24)}", Name="GetSolicitud")]
        [Route("[action]")]
        public ActionResult<Solicitud> GetSolicitud(string id){
            var solicitud = _solicitudService.Get(id);
            if(solicitud==null){
                return NotFound();
            }
            return solicitud;
        }

        [HttpPost]
        public ActionResult<Solicitud> Create(Solicitud solicitud){
            if(solicitud==null/*_solicitudService.checkFecha(solicitud.placa, solicitud.entrada)<=2*/){
                return NoContent();
            }
            _solicitudService.Create(solicitud);
            return CreatedAtRoute("GetSolicitud", new {id = solicitud.Id.ToString()}, solicitud);
        }
        [HttpPut]
        public IActionResult Update(string id, Solicitud solicitudIn){
            var solicitud = _solicitudService.Get(id);
            if(solicitud==null){
                return NotFound();
            }
            _solicitudService.Update(id, solicitudIn);
            return Ok();
        }
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id){
            var solicitud = _solicitudService.Get(id);
            if(solicitud==null){
                return NotFound();
            }
            _solicitudService.Remove(solicitud.Id);
            return Ok();
        }
    }
}
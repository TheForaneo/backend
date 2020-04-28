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

namespace webapi.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : Controller{
        private readonly SolicitudService _solicitudService;

        public SolicitudController(SolicitudService solicitudService){
            _solicitudService = solicitudService;
        }

        [HttpGet]
        public ActionResult<List<Solicitud>> Get() => _solicitudService.Get();

        [HttpGet("{id:length(24)}", Name="GetSolicitud")]
        public ActionResult<Solicitud> Get(string id){
            var solicitud = _solicitudService.Get(id);
            if(solicitud==null){
                return NotFound();
            }
            return solicitud;
        }

        [HttpPost]
        public ActionResult<Solicitud> Create(Solicitud solicitud){
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
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id){
            var solicitud = _solicitudService.Get(id);
            if(solicitud==null){
                return NotFound();
            }
            _solicitudService.Remove(solicitud.Id);
            return NoContent();
        }
    }
}
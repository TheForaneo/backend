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

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : Controller{
        private readonly VehiculoService _vehiculoService;
        public VehiculoController(VehiculoService vehiculoService){
            _vehiculoService=vehiculoService;
        }
        [HttpGet]
        public ActionResult<List<Vehiculo>> Get() => _vehiculoService.Get();
        [HttpGet("{id:length(24)}", Name="GetByCliente")]
        [Route("[action]")]
        public ActionResult<List<Vehiculo>> GetByCliente(string cid) => _vehiculoService.GetByCliente(cid);

        [HttpGet("{id:length(24)}", Name="Get")]
        [Route("action")]
        public ActionResult<Vehiculo> Get(string id){
            var vehiculo=_vehiculoService.Get(id);
            if(vehiculo==null){
                return NotFound();
            }
            return vehiculo;
        }
        [HttpPost]
        //[Authorize]
        public ActionResult<Vehiculo> Create(Vehiculo vehiculo){
            if(_vehiculoService.checkV(vehiculo.placa)==1){
                return NoContent();
            }
            _vehiculoService.Create(vehiculo);
            return CreatedAtRoute("GetVehiculo", new{id=vehiculo.Id.ToString()}, vehiculo);
        }

        [HttpPut]
        //[Authorize]
        public IActionResult Update(string id, Vehiculo vehiculoIn){
            var vehiculo = _vehiculoService.Get(id);
            if(vehiculo == null){
                return NotFound();
            }
            _vehiculoService.Update(id, vehiculoIn);
            return Ok();
        }
        [HttpDelete("{id:length(24)}")]
        [Authorize]
        public IActionResult Delete(string id){
            var vehiculo = _vehiculoService.Get(id);
            if(vehiculo == null){
                return NotFound();
            }
            var filter = Builders<BsonDocument>.Filter.Eq("id",id);
            _vehiculoService.Remove(vehiculo.Id);
            return Ok();
        }
    }
}
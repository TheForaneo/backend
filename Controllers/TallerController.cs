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
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class TallerController : Controller{
        private readonly TallerService _tallerService ;
        private readonly ClienteService _clienteService;

        public TallerController(TallerService tallerService, ClienteService clienteService){
            _tallerService=tallerService;
            _clienteService = clienteService;
        }
        
        [HttpGet]
        public ActionResult<List<Taller>> Get() => _tallerService.Get();
        
        [HttpGet("porTipo/{tipo}", Name="GetTipo")]
        public ActionResult<Taller> GetPorTipo(string tipo){
            var list = _tallerService.BuscarPorTaller(tipo);
            if(list != null){
                return Ok(list);
            }
            return NotFound();
        }
        [HttpGet("{id:length(24)}", Name="GetTaller")]
        public ActionResult<Taller> Get(string id){
            var taller = _tallerService.Get(id);
            if(taller == null){
                return NotFound();
            }
            return taller;
        }
        [HttpGet("porNombre/{nombre}", Name="GetNombre")]
        public ActionResult<Taller> GetNombre(string nombre){
            var taller = _tallerService.GetByName(nombre);
            if(taller == null){
                return NotFound();
            }
            return taller;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Taller> Create(Taller taller){
            var cli = _clienteService.GetCorreo(taller.correo);
            var tal = _tallerService.GetCorreo(taller.correo);
            if(cli != null || tal != null){
                return BadRequest();
            }
            _tallerService.Create(taller);
            return CreatedAtRoute("GetTaller", new {id = taller.Id.ToString()}, taller);
        }
       
        [HttpPut]
        public IActionResult Update(string id, Taller tallerIn){
            var taller = _tallerService.Get(id);
            if(taller == null){
                return NotFound();
            }
            _tallerService.Update(id, tallerIn);
            taller = _tallerService.Get(id);
            return Ok(taller);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id){
            var taller = _tallerService.Get(id);
            if(taller==null){
                return NotFound();
            }
            _tallerService.Remove(taller.Id);
            return Ok();
        }
    }
}
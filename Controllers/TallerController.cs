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
    public class TallerController : Controller{
        private readonly TallerService _tallerService ;

        public TallerController(TallerService tallerService){
            _tallerService=tallerService;
        }

        [HttpGet]
        public ActionResult<List<Taller>> Get() => _tallerService.Get();

        [HttpGet("{id:length(24)}", Name="GetTaller")]
        public ActionResult<Taller> Get(string id){
            var taller = _tallerService.Get(id);
            if(taller == null){
                return NotFound();
            }
            return taller;
        }

        [HttpPost("create")]
        public ActionResult<Taller> Create(Taller taller){
            if(_tallerService.checkCorreo(taller.correo)){
                return NoContent();
            }
            if(_tallerService.checkCelular(taller.celular)){
                return NoContent();
            }
            _tallerService.Create(taller);
            return CreatedAtRoute("GetTaller", new {id = taller.Id.ToString()}, taller);
        }
        [HttpPost("login")]
        public ActionResult inicio(UserLogin oj){
            var user = _tallerService.iniciaSesion(oj.Email, oj.Password);
            if(user != null){
                return Ok();
            }
            return NotFound();
            //return RedirectToAction("inicio");
        }
        [HttpPut]
        [Authorize]
        public IActionResult Update(string id, Taller tallerIn){
            var taller = _tallerService.Get(id);
            if(taller == null){
                return NotFound();
            }
            _tallerService.Update(id, tallerIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize]
        public IActionResult Delete(string id){
            var taller = _tallerService.Get(id);
            if(taller==null){
                return NotFound();
            }
            _tallerService.Remove(taller.Id);
            return NoContent();
        }
    }
}
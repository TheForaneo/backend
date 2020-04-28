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

        [HttpPost]
        public ActionResult<Taller> Create(Taller taller){
            _tallerService.Create(taller);

            return CreatedAtRoute("GetCliente", new {id = taller.Id.ToString()}, taller);
        }

        [HttpPut]
        public IActionResult Update(string id, Taller tallerIn){
            var taller = _tallerService.Get(id);
            if(taller == null){
                return NotFound();
            }
            _tallerService.Update(id, tallerIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
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
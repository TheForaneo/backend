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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace webapi.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ComentariosController:Controller{
        private readonly ComentarioService _comentarios;

        public ComentariosController(ComentarioService comentarioService){
            _comentarios=comentarioService;
        }
        
        [HttpGet("{id}", Name="GetCT")]
        public ActionResult<List<Comentario>> GetCT(string id){
            return _comentarios.GetporTaller(id);
        }
        [HttpGet("Comentario/{idComentario}", Name="GetComentario")]
        public ActionResult<Comentario> GetComentario(string idComentario){
            return _comentarios.GetComentario(idComentario);
        } 
        
        [HttpPost]
        public ActionResult<Comentario> Create(Comentario comentario){
            _comentarios.Create(comentario);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(string id, Comentario comentario){
            var exist = _comentarios.GetComentario(id);
            if(exist == null){
                return NotFound();
            }
            _comentarios.Update(id, comentario);
            return Ok();
        }
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id){
            var exist = _comentarios.GetComentario(id);
            if(exist==null){
                return NotFound();
            }
            _comentarios.Remove(exist.Id);
            return NoContent();
        }
    }
    
}
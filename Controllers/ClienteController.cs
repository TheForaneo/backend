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
    public class ClienteController:Controller{
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService){
            _clienteService=clienteService;
        }
        
        [HttpGet]
        //[Authorize]
        public ActionResult<List<Cliente>> Get() => _clienteService.Get();
        
        [HttpGet("{id:length(24)}", Name="GetCliente")]
        //[Authorize]
        public ActionResult<Cliente> Get(string id){
            var cliente = _clienteService.Get(id);
            if(cliente == null){
                return NotFound();
            }
            return cliente;
        }
        
        [HttpPost]
        public ActionResult<Cliente> Create(Cliente cliente){
            if(_clienteService.checkCorreo(cliente.correo) || _clienteService.checkCelular(cliente.celular)){
                return NoContent();
            }
            _clienteService.Create(cliente);
            return CreatedAtRoute("GetCliente", new {id = cliente.Id.ToString()}, cliente);
        }
        /*
        [HttpPost("login")]
        public ActionResult inicio(UserLogin oj){
            
            return NotFound();
            //return RedirectToAction("inicio");
        }
        */
        [HttpPut]
        //[Authorize]
        public IActionResult Update(string id, Cliente clienteIn){
            var cliente = _clienteService.Get(id);
            if(cliente == null){
                return NotFound();
            }
            _clienteService.Update(id, clienteIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        //[Authorize]
        public IActionResult Delete(string id){
            var cliente = _clienteService.Get(id);
            if(cliente==null){
                return NotFound();
            }
            _clienteService.Remove(cliente.Id);
            return NoContent();
        }
    }
    
}
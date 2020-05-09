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
    public class ClienteController:Controller{
        private readonly ClienteService _clienteService;
        private readonly TallerService _tallerService;

        public ClienteController(ClienteService clienteService, TallerService tallerService){
            _clienteService=clienteService;
            _tallerService = tallerService;
        }
        /*
        [HttpPost]
        public ActionResult<Cliente> GetCorreo(Cliente cliente){
            var client = _clienteService.GetCorreo(cliente.correo);
            return client;
        }
        */
        public ActionResult<List<Cliente>> Get() => _clienteService.Get();
        
        [HttpGet("{id:length(24)}", Name="GetCliente")]
        public ActionResult<Cliente> Get(string id){
            var cliente = _clienteService.Get(id);
            if(cliente == null){
                return NotFound();
            }
            return cliente;
        }
        [HttpGet("{correo}", Name="GetID")]
        public string GetID(string correo){
            string correos =  correo;
            if(!(correos.Equals(null))){
                return _clienteService.GetId(correos);
            }
            return null;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Cliente> Create(Cliente cliente){
            string correo= cliente.correo.ToString();
            var cli = _clienteService.GetCorreo(correo);
            var tal = _tallerService.GetCorreo(correo);
            if(cli!=null || tal!=null){
                return BadRequest();
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
        public IActionResult Update(string id, Cliente clienteIn){
            var cliente = _clienteService.Get(id);
            if(cliente == null){
                return NotFound();
            }
            _clienteService.Update(id, clienteIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
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
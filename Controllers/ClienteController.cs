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
    public class ClienteController:Controller{
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService){
            _clienteService=clienteService;
        }
        [HttpGet]
        public ActionResult<List<Cliente>> Get() => _clienteService.Get();

        [HttpGet("{id:length(24)}", Name="GetCliente")]
        public ActionResult<Cliente> Get(string id){
            var cliente = _clienteService.Get(id);
            if(cliente == null){
                return NotFound();
            }
            return cliente;
        }

        [HttpPost]
        public ActionResult<Cliente> Create(Cliente cliente){
            _clienteService.Create(cliente);

            return CreatedAtRoute("GetCliente", new {id = cliente.Id.ToString()}, cliente);
        }

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
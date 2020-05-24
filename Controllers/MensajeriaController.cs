using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MensajeriaController : Controller{
        private readonly MensajeriaService _mensajeria;   
        DateTime date = new DateTime();
        public MensajeriaController(MensajeriaService mensajeria){
            _mensajeria = mensajeria;
        }

        [HttpGet("{idCliente:length(24)}/{idTaller:length(24)}", Name="GetMensajesClienteTaller")]
        public ActionResult<List<Mensajeria>> GetMensajesClienteTaller(string idCliente, string idTaller){
            return _mensajeria.GetMensajesRecibidosClienteTaller(idCliente, idTaller);
            
        }
        
        //[Route("[action]/")]
        [HttpGet("{id:length(24)}", Name="GetMensajes")]
        public ActionResult<List<Mensajeria>> GetMensajes(string id){
            List<Mensajeria> clientes= _mensajeria.GetMensajesCliente(id);
            List<Mensajeria> taller = _mensajeria.GetMensajesTaller(id);
            if(clientes.Count != 0){
                return _mensajeria.GetMensajesCliente(id);
            }
            if(taller.Count != 0){
                return _mensajeria.GetMensajesTaller(id);
            }
            return null;
        }
        public void EnviaraCliente(Mensajeria Mensaje){
            Mensaje.FechaEnvio=DateTime.Now;
            _mensajeria.EnviarMensaje(Mensaje);
        }
    }
}
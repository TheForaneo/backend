using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using System;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller{
        private readonly ClienteService _clienteService;
        public AccountController(ClienteService clienteService){
            _clienteService=clienteService;
        }
        [HttpPost]
        public ActionResult news(ForgotPassword mod){
            var user = _clienteService.GetCorreo(mod.Email.ToString());
            if(user != null){
                this.SendCorreo(user);
                return Ok();
            }
            return BadRequest();
        }
        public void SendCorreo(Cliente user){
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            if(_clienteService.insertCodigo(user, finalString)){
                var message = new MimeMessage();
                var bodyBuilder = new BodyBuilder();

                message.From.Add(new MailboxAddress("MechaFinder Support", "MechaFinderSupp@gmail.com"));

                message.To.Add(new MailboxAddress("To:", user.correo));

                message.ReplyTo.Add(new MailboxAddress("reply_to", "MechaFinderSupp@gmail.com"));

                message.Subject = "subject";
                bodyBuilder.HtmlBody= "Tu codigo para reiniciar tu contraseña es: "+finalString;
                message.Body = bodyBuilder.ToMessageBody();

                var client = new SmtpClient();

                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate("MechaFinderSupp@gmail.com", "mechafinder123");
                client.Send(message);
                client.Disconnect(true);
            }
        }

    }
}
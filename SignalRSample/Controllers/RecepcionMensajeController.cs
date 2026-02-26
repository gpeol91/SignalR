using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRSample.Hubs;
using SignalRSample.Modelos;

namespace SignalRSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecepcionMensajeController : ControllerBase
    {
        //Para mandar a llamar desde el backend se necesita usar IHubContext para usar un hub creado
        private readonly IHubContext<UserHub> _hubContext;

        public RecepcionMensajeController(IHubContext<UserHub> hubContext)
        {
            _hubContext = hubContext;
        }


        //EJEMPLO DE COMO SE MANDARIA A LLAMAR DESDE UN ENDPOINT
        [HttpGet("recibir")]
        public async Task<IActionResult> Get()
        {
            UserHub.TotalViews++;

            await _hubContext.Clients.All
                .SendAsync("updateTotalViews", UserHub.TotalViews);

            return Ok(new { mensaje = "Se recibio un mensaje" });
        }

        [HttpPost("enviarMensaje")]
        public async Task<IActionResult> enviar([FromBody] mdl_Obtener_Chat_Mensajes mensaje)
        {

            var chatId = mensaje.numeroTelefono;
            await _hubContext.Clients.Group(chatId)
                .SendAsync("ReceiveMessage", mensaje);

            return Ok(new { mensaje = "Se envio el mensaje" });
        }
    }
}

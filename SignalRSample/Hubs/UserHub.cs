using Microsoft.AspNetCore.SignalR;
using SignalRSample.Modelos;
using System.Collections.Concurrent;

namespace SignalRSample.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalViews { get; set; } = 0;

        public static ConcurrentDictionary<string, int> ChatCounters
        = new();

        // 👇 contador global (si lo necesitas)
        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
        }

        // 👇 solo une al grupo
        public async Task JoinChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        // 👇 solo sale del grupo
        public async Task LeaveChat(string chatId)
        {
            var count = ChatCounters.AddOrUpdate(chatId, 1, (_, v) => v - 1);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);

            await Clients.Group(chatId)
                .SendAsync("ReceiveStatus", count);

        }

        // 👇 NUEVO: usuario entró al chat
        public async Task UserEnteredChat(string chatId)
        {
            var count = ChatCounters.AddOrUpdate(chatId, 1, (_, v) => v + 1);

            await Clients.Group(chatId)
                .SendAsync("ReceiveStatus", count);
        }

        // 👇 NUEVO: usuario entró al chat
        public async Task envioMensaje(string chatId, mdl_Obtener_Chat_Mensajes mensaje)
        {

            await Clients.Group(chatId)
                .SendAsync("ReceiveMessage", mensaje);
        }

    }
}

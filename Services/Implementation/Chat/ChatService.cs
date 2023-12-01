using GroupTracker.data;
using GroupTracker.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GroupTracker.Services.Implementation.Chat
{
    public class ChatService
    {
        private readonly DataContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatService(DataContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task SendMessage(string senderId, string receiverUserId, string message)
        {
            var chatMessage = new ChatMessage
            {
                SenderId = senderId,
                ReceiverId = receiverUserId,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            Console.WriteLine("Message sent");
            Console.WriteLine("Sender ID: " + senderId);
            Console.WriteLine("Receiver ID: " + receiverUserId);
            Console.WriteLine("Message: " + message);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", senderId, message);

            Console.WriteLine("Message sent");
        }


    }
}

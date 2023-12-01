using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using GroupTracker.Services.Implementation.Chat;

public class ChatHub : Hub
{
    private readonly ChatService _chatService;

    public ChatHub(ChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task SendMessage(string receiverUserId, string message, string senderId)
    {
        await _chatService.SendMessage(senderId, receiverUserId, message);
    }
}
    
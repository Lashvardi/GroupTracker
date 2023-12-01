using GroupTracker.Services.Implementation.Chat;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public static readonly Dictionary<string, string> UserConnections = new Dictionary<string, string>();
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

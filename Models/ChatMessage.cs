namespace GroupTracker.Models;

public class ChatMessage
{
    public int Id { get; set; }
    public string SenderId { get; set; } // ID of the sender
    public string ReceiverId { get; set; } // ID of the receiver
    public string Message { get; set; }
    public DateTime Timestamp { get; set; } // Time when the message was sent
}

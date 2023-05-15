using SignalRChatTemplete.Models.DTOs;

namespace SignalRTemplete.Hubs.interfaces
{
    public interface IChatHub
    {
        Task JoinGroup(int UserID, int ToUserID);
        Task LeaveGroup(int UserID, int ToUserID);
        Task OnConnectedAsync();
        Task OnDisconnectedAsync(Exception ex);
        Task SendGroupMessage(SendGroupMessageDTO input);
        Task SendGlobeMessage(SendMessageDTO input);
        Task SendPrivateMessage(SendPrivateMessageDTO input);
    }
}
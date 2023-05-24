using SignalRChatTemp.Models.DTOs;

namespace SignalRTemplete.Hubs.interfaces
{
    public interface IChatHub
    {
        Task JoinGroup(int ToUserID);
        Task LeaveGroup(int ToUserID);
        Task OnConnectedAsync();
        Task OnDisconnectedAsync(Exception ex);
        Task SendMessage(SendMessageDTO input);
    }
}
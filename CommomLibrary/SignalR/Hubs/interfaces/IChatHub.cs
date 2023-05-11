namespace CommomLibrary.SignalR.Hubs.interfaces
{
    public interface IChatHub
    {
        Task SendMessage(string selfID, string message, string sendToID);
    }
}
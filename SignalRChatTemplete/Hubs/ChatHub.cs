using CommomLibrary.Dapper.Repository.interfaces;
using Dapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SignalRChatTemplete.DBContexts.Dapper;
using SignalRChatTemplete.Models.DTOs;
using SignalRChatTemplete.Models.Entities;
using System.Data;
using System.Text.Json.Nodes;

namespace SignalRChatTemplete.Hubs
{
    public class ChatHub : Hub
    {
        // 用戶連線 ID 列表
        private static Dictionary<int, string> ConnIDList = new Dictionary<int, string>();
        private readonly ILogger<ChatHub> _logger;
        private readonly IDbConnection _dbConnection;
        private readonly int ConnectionLimit = 100;

        public ChatHub(ILogger<ChatHub> logger, IConfiguration configuration, IBaseDapper<ProjectDBContext> baseDapper)
        {
            _logger = logger;
            _dbConnection = baseDapper.CreateConnection();
            ConnectionLimit = configuration.GetValue<int>("ConnectionLimit");
        }

        /// <summary>
        /// 連線事件
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync() 
        {
            //檢查線上連線樹是否超過設定
            if (ConnIDList.Count > ConnectionLimit)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("UpdContent", "連線數量已達上限");
                return;
            }
            // 取得連線ID
            string connID = Context.ConnectionId;
            // 取得使用者ID
            string userID = Context.User.Identity.Name;
            // 更新連線ID
            ConnIDList.Add(Convert.ToInt32(userID), connID);
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 離線事件
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex) 
        {
            // 取得使用者ID
            string userID = Context.User.Identity.Name;
            // 移除連線ID
            ConnIDList.Remove(Convert.ToInt32(userID));
            await base.OnDisconnectedAsync(ex);
        }








        public async Task SendMessage(string selfID, string message, string sendToID)
        {
            if (string.IsNullOrEmpty(selfID))
            {
                await Clients.All.SendAsync("UpdContent", "你沒有連線");
            }
            else if (string.IsNullOrEmpty(sendToID))
            {
                await Clients.All.SendAsync("UpdContent", selfID + " 說: " + message);
            }
            else
            {
                // 接收人
                await Clients.Client(sendToID).SendAsync("UpdContent", selfID + " 私訊向你說: " + message);

                // 發送人
                await Clients.Client(Context.ConnectionId).SendAsync("UpdContent", "你向 " + sendToID + " 私訊說: " + message);
            }
        }

        public async Task SendMessage_JSON(string user, string message)
        {
            SendMessageDTO responseJson = new SendMessageDTO();
            responseJson.UserID = user;
            responseJson.Message = message;
            await Clients.All.SendAsync("UpdContent", responseJson);
        }

        /// <summary>
        /// 加入群組
        /// </summary>
        /// <param name="selfID"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task JoinGroup(string selfID, string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            // 更新聊天內容
            await Clients.All.SendAsync("UpdContent", selfID + "已加入群組: " + groupName);
        }

        /// <summary>
        /// 離開群組
        /// </summary>
        /// <param name="selfID"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task LeaveGroup(string selfID, string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            // 更新聊天內容
            await Clients.All.SendAsync("UpdContent", selfID + "已離開群組: " + groupName);
        }

        /// <summary>
        /// 群組傳送訊息
        /// </summary>
        /// <param name="selfID"></param>
        /// <param name="message"></param>
        /// <param name="sendToGroup"></param>
        /// <returns></returns>
        public async Task SendGroupMessage(string selfID, string message, string sendToGroup)
        {
            await Clients.Group(sendToGroup).SendAsync("UpdContent", selfID + " 說: " + message);
        }
    }
}

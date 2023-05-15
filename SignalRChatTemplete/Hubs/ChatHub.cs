using CommomLibrary.Dapper.Repository.interfaces;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRChatTemplete.DBContexts.Dapper;
using SignalRChatTemplete.Models.DTOs;
using SignalRChatTemplete.Models.Entities;
using SignalRTemplete.Hubs.interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace SignalRChatTemplete.Hubs
{
    [Authorize(Roles = "Admin,User")]
    public class ChatHub : Hub, IChatHub
    {
        // 用戶連線 ID 列表
        private static Dictionary<int, string> ConnIDList = new Dictionary<int, string>();
        private static Dictionary<int, string> AvalibleGroupIDList = new Dictionary<int, string>();
        private readonly ILogger<ChatHub> _logger;
        private readonly IDbConnection _dbConnection;
        private readonly int ConnectionLimit = 100;

        public ChatHub(ILogger<ChatHub> logger, IConfiguration configuration, IBaseDapper<ProjectDBContext_SignalR> baseDapper)
        {
            _logger = logger;
            _dbConnection = baseDapper.CreateConnection();
            ConnectionLimit = configuration.GetValue<int>("ConnectionLimit");
            AvalibleGroupIDList = _dbConnection.Query<GroupInfo>("SELECT GroupID, GroupName FROM GroupInfo WHERE IsEnable = 1").ToDictionary(x => x.GroupID, x => x.GroupName);
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
            string userID = Context.User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).Select(x => x.Value).FirstOrDefault();
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

        /// <summary>
        /// 全頻訊息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task SendGlobeMessage(SendMessageDTO input)
        {
            if (ConnIDList.Any(x => x.Key == input.UserID))
            {
                ResponceDTO responce = new ResponceDTO()
                {
                    UserID = input.UserID,
                    UserName = "",//要在快取中取得名稱
                    Message = input.Message,
                    SendTime = DateTime.Now
                };
                await Clients.All.SendAsync("UpdContent", responce);
            }
        }
        /// <summary>
        /// 群組訊息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task SendGroupMessage(SendGroupMessageDTO input)
        {
            if (ConnIDList.Any(x => x.Key == input.UserID) && AvalibleGroupIDList.Any(x => x.Key == input.ToGroupID))
            {
                ResponceDTO responce = new ResponceDTO()
                {
                    UserID = input.UserID,
                    UserName = ConnIDList.First(x => x.Key == input.UserID).Value,
                    Message = input.Message,
                    SendTime = DateTime.Now
                };
                await Clients.Group(AvalibleGroupIDList.First(x => x.Key == input.ToGroupID).Value).SendAsync("UpdContent", responce);
            }
        }
        /// <summary>
        /// 私人訊息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task SendPrivateMessage(SendPrivateMessageDTO input)
        {
            if (ConnIDList.Any(x => x.Key == input.UserID) && ConnIDList.Any(x => x.Key == input.ToUserID))
            {
                ResponceDTO responce = new ResponceDTO()
                {
                    UserID = input.UserID,
                    UserName = ConnIDList.First(x => x.Key == input.UserID).Value,
                    Message = input.Message,
                    SendTime = DateTime.Now
                };
                await Clients.Client(ConnIDList.First(x => x.Key == input.ToUserID).Value).SendAsync("UpdContent", responce);
            }
        }

        /// <summary>
        /// 加入群組
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ToUserID"></param>
        /// <returns></returns>
        public async Task JoinGroup(int UserID, int ToUserID)
        {
            if (ConnIDList.Any(x => x.Key == UserID) && AvalibleGroupIDList.Any(x => x.Key == ToUserID))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, AvalibleGroupIDList.First(x => x.Key == ToUserID).Value);
                // 更新聊天內容
                await Clients.All.SendAsync("UpdContent", "已加入群組: " + AvalibleGroupIDList.First(x => x.Key == ToUserID).Value);
            }
        }

        /// <summary>
        /// 離開群組
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ToUserID"></param>
        /// <returns></returns>
        public async Task LeaveGroup(int UserID, int ToUserID)
        {
            if (ConnIDList.Any(x => x.Key == UserID) && AvalibleGroupIDList.Any(x => x.Key == ToUserID))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, AvalibleGroupIDList.First(x => x.Key == ToUserID).Value);
                // 更新聊天內容
                await Clients.All.SendAsync("UpdContent", "已離開群組: " + AvalibleGroupIDList.First(x => x.Key == ToUserID).Value);
            }
        }
    }
}

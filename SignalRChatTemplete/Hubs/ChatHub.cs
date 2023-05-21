using CommomLibrary.Dapper.Repository.interfaces;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
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
            HubCallerContext context = this.Context;
            //檢查線上連線樹是否超過設定
            if (ConnIDList.Count > ConnectionLimit)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("UpdContent", "連線數量已達上限");
                return;
            }
            // 取得連線ID
            string connID = Context.ConnectionId;
            // 取得使用者ID
            int userID = Int32.Parse(Context.User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value);
            string userName = Context.User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Name).FirstOrDefault().Value;
            // 更新個人 ID
            await Clients.Client(Context.ConnectionId).SendAsync("UpdSelfID", $"使用者:{userName}({userID})");
            // 更新連線 ID 列表
            string jsonString = JsonConvert.SerializeObject(ConnIDList);
            await Clients.All.SendAsync("UpdList", jsonString);
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
            int userID = Int32.Parse(Context.User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value);
            // 移除連線ID
            ConnIDList.Remove(userID);
            await base.OnDisconnectedAsync(ex);
        }

        /// <summary>
        /// 傳送訊息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task SendMessage(SendMessageDTO input)
        {
            int userID = Int32.Parse(Context.User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value);
            string userName = Context.User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Name).FirstOrDefault().Value;
            if (ConnIDList.Any(x => x.Key == userID))
            {
                //私訊
                if (input.ToUserID != null && ConnIDList.Any(x => x.Key == input.ToUserID))
                {
                    ResponceDTO responce = new ResponceDTO()
                    {
                        UserID = userID,
                        UserName = userName,
                        Message = input.Message,
                        SendTime = DateTime.Now
                    };
                    await Clients.Client(ConnIDList.First(x => x.Key == input.ToUserID).Value).SendAsync("UpdContent", responce);
                }
                //群聊
                if (input.ToGroupID != null && AvalibleGroupIDList.Any(x => x.Key == input.ToGroupID))
                {
                    ResponceDTO responce = new ResponceDTO()
                    {
                        UserID = userID,
                        UserName = userName,
                        Message = input.Message,
                        SendTime = DateTime.Now
                    };
                    await Clients.Group(AvalibleGroupIDList.First(x => x.Key == input.ToGroupID).Value).SendAsync("UpdContent", responce);
                }
                //廣播
                if (input.ToUserID == null && input.ToGroupID == null)
                {
                    ResponceDTO responce = new ResponceDTO()
                    {
                        UserID = userID,
                        UserName = userName,
                        Message = input.Message,
                        SendTime = DateTime.Now
                    };
                    await Clients.All.SendAsync("UpdContent", responce);
                }
            }
        }

        /// <summary>
        /// 加入群組
        /// </summary>
        /// <param name="ToUserID"></param>
        /// <returns></returns>
        public async Task JoinGroup(int ToUserID)
        {
            int userID = Int32.Parse(Context.User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value);
            if (ConnIDList.Any(x => x.Key == userID) && AvalibleGroupIDList.Any(x => x.Key == ToUserID))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, AvalibleGroupIDList.First(x => x.Key == ToUserID).Value);
                // 更新聊天內容
                await Clients.All.SendAsync("UpdContent", "已加入群組: " + AvalibleGroupIDList.First(x => x.Key == ToUserID).Value);
            }
        }

        /// <summary>
        /// 離開群組
        /// </summary>
        /// <param name="ToUserID"></param>
        /// <returns></returns>
        public async Task LeaveGroup(int ToUserID)
        {
            int userID = Int32.Parse(Context.User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value);
            if (ConnIDList.Any(x => x.Key == userID) && AvalibleGroupIDList.Any(x => x.Key == ToUserID))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, AvalibleGroupIDList.First(x => x.Key == ToUserID).Value);
                // 更新聊天內容
                await Clients.All.SendAsync("UpdContent", "已離開群組: " + AvalibleGroupIDList.First(x => x.Key == ToUserID).Value);
            }
        }
    }
}

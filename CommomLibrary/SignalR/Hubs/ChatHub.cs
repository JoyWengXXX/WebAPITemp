using CommomLibrary.SignalR.Hubs.interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CommomLibrary.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        // 用戶連線 ID 列表
        public static List<string> ConnIDList = new List<string>();

        /// <summary>
        /// 連線事件
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            if (ConnIDList.Where(p => p == Context.ConnectionId).FirstOrDefault() == null)
            {
                ConnIDList.Add(Context.ConnectionId);
            }
            if(ConnIDList.Count > 3)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("UpdContent", "連線人數已滿");

                await base.OnDisconnectedAsync(null);
            }
            else
            {
                // 更新連線 ID 列表
                string jsonString = JsonConvert.SerializeObject(ConnIDList);
                await Clients.All.SendAsync("UpdList", jsonString);

                // 更新個人 ID
                await Clients.Client(Context.ConnectionId).SendAsync("UpdSelfID", Context.ConnectionId);

                // 更新聊天內容
                await Clients.All.SendAsync("UpdContent", "新連線 ID: " + Context.ConnectionId);

                await base.OnConnectedAsync();
            }
        }

        /// <summary>
        /// 離線事件
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            string id = ConnIDList.Where(p => p == Context.ConnectionId).FirstOrDefault();
            if (id != null)
            {
                ConnIDList.Remove(id);
            }
            // 更新連線 ID 列表
            string jsonString = JsonConvert.SerializeObject(ConnIDList);
            await Clients.All.SendAsync("UpdList", jsonString);

            // 更新聊天內容
            await Clients.All.SendAsync("UpdContent", "已離線 ID: " + Context.ConnectionId);

            await base.OnDisconnectedAsync(ex);
        }

        /// <summary>
        /// 傳遞訊息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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



        public async Task SendMessage_JSON(string user, string message)
        {
            ReponseJson responseJson = new ReponseJson();
            responseJson.user = user;
            responseJson.message = message;
            await Clients.All.SendAsync("UpdContent", responseJson);
        }
    }





    public class ReponseJson
    {
        public int id { get; set; }
        public string user { get; set; }
        public string message { get; set; }
        public string group { get; set; }
    }
}

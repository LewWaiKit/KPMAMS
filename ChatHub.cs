using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KPMAMS
{

    public class ChatHub : Hub
    {
        static List<Users> ConnectedUsers = new List<Users>();
        static List<Messages> CurrentMessage = new List<Messages>();
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                string UserImg = GetUserImage(userName);
                string logintime = DateTime.Now.ToString();
                ConnectedUsers.Add(new Users { ConnectionId = id, UserName = userName, UserImage = UserImg, LoginTime = logintime });

                // send to caller
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName, UserImg, logintime);
            }
        }

        public void SendMessageToAll(string userName, string message, string time)
        {
            string UserImg = GetUserImage(userName);
            // store last 100 messages in cache
            AddMessageinCache(userName, message, time, UserImg);

            // Broad cast message
            Clients.All.messageReceived(userName, message, time, UserImg);

        }

        private void AddMessageinCache(string userName, string message, string time, string UserImg)
        {
            CurrentMessage.Add(new Messages { UserName = userName, Message = message, Time = time, UserImage = UserImg });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);

            // Refresh();
        }

        public string GetUserImage(string username)
        {
            string RetimgName = "img/logoKPM.png";
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT ProfilePic from Teacher where FullName='" + username + "'", con);

                SqlDataReader dr = cmd.ExecuteReader();

                String ImageName = "";

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ImageName = dr.GetValue(0).ToString();        
                    }
                }

                if (ImageName != "")
                    RetimgName = ImageName;

                con.Close();
            }
            catch (Exception ex)
            {
                
            }
            return RetimgName;
        }


        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }
            return base.OnDisconnected(stopCalled);
        }

        public void SendPrivateMessage(string toUserId, string message)
        {

            string fromUserId = Context.ConnectionId;

            var toUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

            if (toUser != null && fromUser != null)
            {
                string CurrentDateTime = DateTime.Now.ToString();
                string UserImg = GetUserImage(fromUser.UserName);
                // send to 
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, UserImg, CurrentDateTime);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message, UserImg, CurrentDateTime);
            }

        }
    }
}
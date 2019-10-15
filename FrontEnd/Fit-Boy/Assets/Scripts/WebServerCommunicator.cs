using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Text;
using System;
using System.Windows;
using System.Collections.Specialized;

public class WebServerCommunicator
{
    private const string WEB_SERVER_ADDRESS = "https://fitboy.tk/";
    private string ErrorMessage;

    private JSONResponse WebCommunication(string serverPage, NameValueCollection data)
    {
        using (WebClient client = new WebClient())
        {
            byte[] responsebytes = new byte[0];
            try
            {
                responsebytes = client.UploadValues(WEB_SERVER_ADDRESS + serverPage + ".php", "POST", data);
            }catch(WebException e)
            {
                Debug.Log(e);
                JSONResponse errorResponse = new JSONResponse(false);
                ErrorMessage = errorResponse.message;
                return errorResponse;
            }
            string responsebody = Encoding.UTF8.GetString(responsebytes);
            Debug.Log(responsebody);
            JSONResponse response = new JSONResponse(responsebody);
            Debug.Log(response.ToString());
            ErrorMessage = response.message;
            return response;
        }
    }

    public bool RegisterUser(string username, string email, string password)
    {
        var data = new System.Collections.Specialized.NameValueCollection();
        data.Add("username", username);
        data.Add("email", email);
        data.Add("password", password);
        return !WebCommunication("register", data).error;
    }

    public User AuthenticateUser(string username, string password)
    {
        return AuthenticateUser(username, password, false);
    }

    public User AuthenticateUser(string username, string password, bool remembered)
    {
        var data = new System.Collections.Specialized.NameValueCollection();
        data.Add("username", username);
        data.Add("password", password);
        if (remembered)
        {
            data.Add("remembered", "true");
        }
        else
        {
            data.Add("remembered", "false");
        }
        return WebCommunication("authenticate_user", data).user;
    }

    public bool UploadQuest(Quest quest, User user)
    {
        var data = new System.Collections.Specialized.NameValueCollection();
        data.Add("username", user.GetUsername());
        data.Add("user_id", "" + user.GetUserID());
        data.Add("quest_name", quest.info.Title);
        data.Add("quest_description", quest.info.Desc);
        data.Add("quest_xp", "" + quest.Xp_reward);
        data.Add("quest_level", "" + quest.Level);
        data.Add("start_lat", "" + quest.Start_co.Lat);
        data.Add("start_long", "" + quest.Start_co.Lon);
        data.Add("end_lat", "" + quest.Stop_co.Lat);
        data.Add("end_long", "" + quest.Stop_co.Lon);

        return !WebCommunication("upload_quest", data).error;
    }

    public bool UpdateUser(User user)
    {
        return UpdateUser(user, "", "", "");
    }

    public bool UpdateUser(User user, string newEmail, string oldPassword, string newPassword)
    {
        var data = new System.Collections.Specialized.NameValueCollection();
        data.Add("username", user.GetUsername());
        data.Add("new_email", user.GetEmail());
        data.Add("xp", "" + user.GetXp());
        data.Add("level", "" + user.GetLevel());
        data.Add("old_password", oldPassword);
        data.Add("new_password", newPassword);

        return !WebCommunication("upload_user", data).error;
    }

    public bool DeleteUser(string username)
    {
        var data = new System.Collections.Specialized.NameValueCollection();
        data.Add("username", username);

        return !WebCommunication("delete_user", data).error;
    }

    public bool AddFriend(string username, string friendUsername)
    {
        var data = new System.Collections.Specialized.NameValueCollection();
        data.Add("username", username);
        data.Add("friend_username", friendUsername);

        return !WebCommunication("add_friend", data).error;
    }

    public bool DeleteFriend(string username, string friendUsername)
    {
        var data = new System.Collections.Specialized.NameValueCollection();
        data.Add("username", username);
        data.Add("friend_username", friendUsername);

        return !WebCommunication("delete_friend", data).error;
    }

    public LinkedList<User> GetFriends(int user_id)
    {
        var data = new System.Collections.Specialized.NameValueCollection();
        data.Add("user_id", "" + user_id);

        return WebCommunication("get_friends", data).friends;
    }

    public bool ResetPassword(string email)
    {
        var data = new System.Collections.Specialized.NameValueCollection();
        data.Add("email", email);

        return !WebCommunication("password_reset", data).error;
    }

    public string GetLastErrorMessage()
    {
        return ErrorMessage;
    }


    private class JSONResponse
    {
        public bool error;
        public string message;
        public User user;
        public LinkedList<User> friends;

        public JSONResponse(bool exception)
        {
            this.error = true;
            this.message = "Error connecting to the webserver";
            user = null;
            friends = null;
        }

        public JSONResponse(string response)
        {
            char[] separators = { '{', '}', '"', ',', ':' };
            string[] items = response.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            this.error = bool.Parse(items[1]);
            this.message = items[3];

            for (int i = 0; i < items.Length; i++)
            {
                Debug.Log(i + " " + items[i]);
            }
            if (items.Length > 4 && items[4].Equals("user"))
            {
                if (!items[5].Equals("null"))
                {
                    user = new User(Int32.Parse(items[6]), items[8], items[10], Int32.Parse(items[12]), Int32.Parse(items[14]));
                }
            }

            //quests
            if(items.Length > 15 && items[15].Equals("quests"))
            {
                LinkedList<Quest> quests = user.GetQuests();
                for(int i = 18; i < items.Length; i += 24)
                {
                    Quest_info qi = new Quest_info(items[i], items[i+2]);
                    quests.AddLast(new Quest(qi, Int32.Parse(items[i+4]), Int32.Parse(items[i+6]), new GPSCoordinate(Double.Parse(items[i+14]), Double.Parse(items[i+16]),""), new GPSCoordinate(Double.Parse(items[i+18]), Double.Parse(items[i+20]),"")));
                }
            }

            //friends
            if(items.Length > 4 && items[4].Equals("friends"))
            {
                friends = new LinkedList<User>();
                for(int i = 7; i < items.Length; i += 6)
                {
                    friends.AddLast(new User(items[i], Int32.Parse(items[i + 2]), Int32.Parse(items[i + 4])));
                }
            }

        }

        public string ToString()
        {
            return "error: " + error + " message: " + message;
        }
    }
    
}

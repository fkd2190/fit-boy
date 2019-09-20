using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Text;
using System;
using System.Windows;

public class WebServerCommunicator
{
    private const string WEB_SERVER_ADDRESS = "https://fitboy.tk/";
    private string ErrorMessage;

    public bool RegisterUser(string username, string email, string password)
    {
        using (WebClient client = new WebClient())
        {
            var data = new System.Collections.Specialized.NameValueCollection();
            data.Add("username", username);
            data.Add("email", email);
            data.Add("password", password);
            byte[] responsebytes = client.UploadValues(WEB_SERVER_ADDRESS + "register.php", "POST", data);
            string responsebody = Encoding.UTF8.GetString(responsebytes);
            JSONResponse response = new JSONResponse(responsebody);
            Debug.Log(response.ToString());
            ErrorMessage = response.message;
            return !response.error;
        }
        return false;
    }

    public User AuthenticateUser(string username, string password)
    {
        using (WebClient client = new WebClient())
        {
            var data = new System.Collections.Specialized.NameValueCollection();
            data.Add("username", username);
            data.Add("password", password);
            byte[] responsebytes = client.UploadValues(WEB_SERVER_ADDRESS + "authenticate_user.php", "POST", data);
            string responsebody = Encoding.UTF8.GetString(responsebytes);
            JSONResponse response = new JSONResponse(responsebody);
            Debug.Log(response.ToString());
            Debug.Log(responsebody);
            ErrorMessage = response.message;
            return response.user;
        }
        return null;
    }

    public bool UploadQuest(Quest quest, User user)
    {
        using (WebClient client = new WebClient())
        {
            var data = new System.Collections.Specialized.NameValueCollection();
            data.Add("username", user.GetUsername());
            data.Add("user_id", "" + user.GetUserID());
            data.Add("quest_name", quest.info.Title);
            data.Add("quest_xp", "" + quest.Xp_reward);
            data.Add("quest_level", "" + quest.Level);
            data.Add("start_lat", "" + quest.Start_co.Lat);
            data.Add("start_long", "" + quest.Start_co.Lon);
            data.Add("end_lat", "" + quest.Stop_co.Lat);
            data.Add("end_long", "" + quest.Stop_co.Lon);
            byte[] responsebytes = client.UploadValues(WEB_SERVER_ADDRESS + "upload_quest.php", "POST", data);
            string responsebody = Encoding.UTF8.GetString(responsebytes);
            JSONResponse response = new JSONResponse(responsebody);
            Debug.Log(response.ToString());
            Debug.Log(responsebody);
            ErrorMessage = response.message;
            return !response.error;
        }
        return false;
    }

    public bool UpdateUser(User user)
    {
        using (WebClient client = new WebClient())
        {
            var data = new System.Collections.Specialized.NameValueCollection();
            data.Add("username", user.GetUsername());
            data.Add("new_email", user.GetEmail());
            data.Add("xp", ""+user.GetXp());
            data.Add("level", ""+user.GetLevel());
            byte[] responsebytes = client.UploadValues(WEB_SERVER_ADDRESS + "upload_user.php", "POST", data);
            string responsebody = Encoding.UTF8.GetString(responsebytes);
            Debug.Log(responsebody);
            JSONResponse response = new JSONResponse(responsebody);
            Debug.Log(response.ToString());
            ErrorMessage = response.message;
            return !response.error;
        }
        return false;
    }

    public bool DeleteUser(string username)
    {
        using (WebClient client = new WebClient())
        {
            var data = new System.Collections.Specialized.NameValueCollection();
            data.Add("username", username);
            byte[] responsebytes = client.UploadValues(WEB_SERVER_ADDRESS + "delete_user.php", "POST", data);
            string responsebody = Encoding.UTF8.GetString(responsebytes);
            Debug.Log(responsebody);
            JSONResponse response = new JSONResponse(responsebody);
            ErrorMessage = response.message;
            return !response.error;
        }
        return false;
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
            if (items.Length > 4)
            {
                if (!items[5].Equals("null"))
                {
                    user = new User(Int32.Parse(items[6]), items[8], items[10], Int32.Parse(items[12]), Int32.Parse(items[14]));
                }
            }

            if(items.Length > 15)
            {
                LinkedList<Quest> quests = user.GetQuests();
                for(int i = 18; i < items.Length; i += 22)
                {
                    Quest_info qi = new Quest_info(items[i], "");
                    quests.AddLast(new Quest(qi, Int32.Parse(items[i + 2]), Int32.Parse(items[i + 4]), new GPSCoordinate(Double.Parse(items[i + 14]), Double.Parse(items[i + 16]), ""), new GPSCoordinate(Double.Parse(items[i + 18]), Double.Parse(items[i + 20]), "")));
                }
            }

        }

        public string ToString()
        {
            return "error: " + error + " message: " + message;
        }
    }
    
}

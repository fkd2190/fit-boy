using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Text;

public class WebServerCommunicator : MonoBehaviour
{
    private const string WEB_SERVER_ADDRESS = "http://www.fitboy.tk/";
    private const string REGISTER_PAGE = "register.php";
    private const string AUTHENTICATE_PAGE = "authenticate.php";
    private const string QUEST_PAGE = "quest.php";

    public bool RegisterUser(string username, string email, string password)
    {
        //using (WebClient client = new WebClient())
        //{
        //    var data = new System.Collections.Specialized.NameValueCollection();
        //    data.Add("username", username);
        //    data.Add("email", email);
        //    data.Add("password", password);
        //    byte[] responsebytes = client.UploadValues(WEB_SERVER_ADDRESS + REGISTER_PAGE, "POST", data);
        //    string responsebody = Encoding.UTF8.GetString(responsebytes);
        //    Debug.Log(responsebody);
        //    return !responsebody.Equals("error");
        //}
        return false;
    }

    public User AuthenticateUser(string username, string password)
    {
        return new User();
    }

    public bool UploadQuest(Quest quest, int userId)
    {
        return false;
    }

    public ArrayList GetUserQuests(int userId)
    {
        return new ArrayList();
    }
}

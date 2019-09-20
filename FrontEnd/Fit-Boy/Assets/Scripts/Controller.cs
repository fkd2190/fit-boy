using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public User user;
    public WebServerCommunicator wsc;

    public Controller()
    {
        wsc = new WebServerCommunicator();
    }

    public User GetUser()
    {
        return user;
    }

    public void SetUser(User user)
    {
        this.user = user;
    }

    public WebServerCommunicator GetWebServerCommunicator()
    {
        return wsc;
    }


}

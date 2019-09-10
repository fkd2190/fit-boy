using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private int userID;
    private string username;
    private string email;
    private int xp;
    private int level;

    public User()
    {
        username = "3135035_fitboy";
        email = "3135035_fitboy@fitboy.tk";
        xp = 10;
        level = 10;
    }

    public User(int user_id, string username, string email, int xp, int level)
    {
        this.userID = userID;
        this.username = username;
        this.email = email;
        this.xp = xp;
        this.level = level;
    }

    public string GetUsername()
    {
        return username;
    }

    public string GetEmail()
    {
        return email;
    }

    public int GetXp()
    {
        return xp;
    }

    public void SetXp(int xp)
    {
        this.xp = xp;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetUserID()
    {
        return userID;
    }
}

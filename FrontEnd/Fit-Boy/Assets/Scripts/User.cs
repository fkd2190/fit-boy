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
    private LinkedList<Quest> userQuests;

    public User()
    {
        username = "3135035_fitboy";
        email = "3135035_fitboy@fitboy.tk";
        xp = 10;
        level = 10;
        userQuests = new LinkedList<Quest>();
    }

    public User(int user_id, string username, string email, int xp, int level)
    {
        this.userID = userID;
        this.username = username;
        this.email = email;
        this.xp = xp;
        this.level = level;
        userQuests = new LinkedList<Quest>();
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

    public void AddXp(int xp)
    {
        if ((this.xp + xp) > 100)
        {
            level++;
            this.xp = (this.xp + xp) - 100;
        }
        else
        {
            this.xp += xp;
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetUserID()
    {
        return userID;
    }

    public LinkedList<Quest> GetQuests()
    {
        return userQuests;
    }
}

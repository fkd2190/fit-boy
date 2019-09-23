﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour
{
    public User user;
    public WebServerCommunicator wsc;
    public Quest activeQuest;


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

    public void SetActiveQuest(Quest quest)
    {
        this.activeQuest = quest;
    }

    public Quest GetActiveQuest()
    {
        return activeQuest;
    }

    public WebServerCommunicator GetWebServerCommunicator()
    {
        return wsc;
    }

    public void CompleteQuest()
    {
        Debug.Log(user.GetUserID());
        wsc.UploadQuest(activeQuest, user);
        user.AddXp(activeQuest.Xp_reward);
        user.GetQuests().AddLast(activeQuest);
        wsc.UpdateUser(user);
        activeQuest = null;
        GameObject.Find("InitClasses").GetComponent<FitBoyGUI>().UpdateProfileGUI(user);
    }

    public bool CheckFinished(GPSCoordinate current, GPSCoordinate dest)
    {
        Debug.Log(current.Lat + " " + current.Lon);
        Debug.Log(dest.Lat + " " + dest.Lon);
        bool arrived = false;
        if (((current.Lat
                    <= (dest.Lat + 0.0001))
                    && (current.Lat
                    >= (dest.Lat - 0.0001))))
        {
            if (((current.Lon
                        <= (dest.Lon + 0.0001))
                        && (current.Lon
                        >= (dest.Lon - 0.0001))))
            {
                arrived = true;
            }

        }

        return arrived;
    }

    public bool CheckFinished(double lat1, double lon1, double lat2, double lon2)
    {
        if ((lat1 == lat2) && (lon1 == lon2))
        {
            return true;
        }
        else
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            //Convert to m
            dist = dist * 1.609344 * 1000;
            Debug.Log(dist);
            return (dist < 10);
        }
    }

    private double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    private double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }
}

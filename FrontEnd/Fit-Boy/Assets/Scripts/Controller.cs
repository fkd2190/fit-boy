using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour
{
    public User user;
    public WebServerCommunicator wsc;
    public Quest activeQuest;
    public GameObject questFinishedPanel;
    public GameObject radiationZonePanel;
    public ArrayList RadiationZones;


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
        StartCoroutine(CompleteQuestCoroutine());
    }

    public IEnumerator CompleteQuestCoroutine()
    {
        questFinishedPanel.SetActive(true);
        yield return null;
        Debug.Log(user.GetUserID());
        wsc.UploadQuest(activeQuest, user);
        if (activeQuest.crossedRadZone)
        {
            activeQuest.Xp_reward *= 2; //double xp if crossed rad zone
        }
        user.AddXp(activeQuest.Xp_reward);
        user.GetQuests().AddLast(activeQuest);
        wsc.UpdateUser(user);
        activeQuest = null;
        GameObject.Find("InitClasses").GetComponent<FitBoyGUI>().UpdateProfileGUI(user);
    }

    public bool CheckFinished(double lat1, double lon1, double lat2, double lon2)
    {
        return (CalculateDistance(lat1, lon1, lat2, lon2) < 20);
    }

    public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        if ((lat1 == lat2) && (lon1 == lon2))
        {
            return 0;
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
            //Debug.Log(dist);
            return dist;
        }
    }

    public bool CheckInZone(double lat1, double lon1)
    {
        foreach(Radiation_Zone zone in RadiationZones)
        {
            double dist = CalculateDistance(lat1, lon1, zone.coordinate.Lat, zone.coordinate.Lon);
            Debug.Log("Distance to zone: " + dist);
            if(dist <= zone.radius)
            {
                radiationZonePanel.SetActive(true);
                return true;
            }
        }
        return false;
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

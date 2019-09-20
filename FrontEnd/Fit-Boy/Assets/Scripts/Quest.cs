using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System;
using System.Windows;

public class Quest
{
    public string title { get; set; }
    public string description { get; set; }
    public float distance { get; set; }
    public int xpLevels { get; set; }
    public int levelRequirements { get; set; }
    public int questID { get; set; }
    public GPSCoordinate startCoordinate { get; set; }
    public GPSCoordinate endCoordinate { get; set; }
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }

    public Quest()
    {

    }

    public Quest(string title, int xp, float distance, string startTime, string endTime, GPSCoordinate startCoordinate, GPSCoordinate endCoordinate)
    {
        this.title = title;
        this.distance = distance;
        this.xpLevels = xp;
        this.startTime = Convert.ToDateTime(startTime);
        this.endTime = Convert.ToDateTime(endTime);
        this.startCoordinate = startCoordinate;
        this.endCoordinate = endCoordinate;
    }
}

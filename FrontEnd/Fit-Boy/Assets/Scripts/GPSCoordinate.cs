using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSCoordinate
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public GPSCoordinate(double latitude, double longitude)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;
    }
}

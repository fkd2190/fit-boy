using System;
using System.Collections;
using UnityEngine;

public class Gen_Rad_Zone
{

	static System.Random rand = new System.Random();

	static ArrayList rads = new ArrayList();

	public static ArrayList In_rad_zone(GPSCoordinate current)
	{
		for (int i = 0; (i < 10); i++)
		{
            //Generate 10 random radiation zones around the user's location
            double randomLatitudeOffset = (rand.NextDouble() * 0.1) - 0.05;
            double randomLongitudeOffset = (rand.NextDouble() * 0.1) - 0.05;

            double zoneLatitide = randomLatitudeOffset + current.Lat;
            double zoneLongitude = randomLongitudeOffset + current.Lon;

            double radius = (rand.NextDouble() * 350) + 50;
            GPSCoordinate gpsCoordinate = new GPSCoordinate(zoneLatitide, zoneLongitude, "Radiation Zone");

			rads.Add(new Radiation_Zone(gpsCoordinate, radius));
		}

        foreach(Radiation_Zone zone in rads)
        {
            Debug.Log("Coordinate: " + zone.coordinate.Lat + ", " + zone.coordinate.Lon + " Radius: " + zone.radius);
        }

        return rads;
	}
}
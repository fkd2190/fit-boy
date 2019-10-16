package Fit_Boy_Quest_engine;
using java.util.ArrayList;
public class Compair_coordinates
{

	public static bool Are_We_There_Yet(Coordinate current, Coordinate dest)
	{
		bool arrived = false;
		if (((current.Lat <= (dest.Lat + 0.001)) && (current.Lat >= (dest.Lat - 0.001))))
		{
			if (((current.Lon <= (dest.Lon + 0.001)) && (current.Lon>= (dest.Lon - 0.001))))
			{
				arrived = true;
			}

		}

		return arrived;
	}

	public static bool In_The_Zone(Coordinate current, ArrayList<Radiation_Zone> radZone)
	{
		bool InZone = false;
		for (int i = 0; (i < radZone.size()); i++)
		{
			if (((current.Lat <= (radZone.get(i).coordinate.Lat + radZone.get(i).radius)) && (current.Lat >= (radZone.get(i).coordinate.Lat - radZone.get(i).radius))))
			{
				if (((current.Lon <= (radZone.get(i).coordinate.Lon + radZone.get(i).radius)) && (current.Lon >= (radZone.get(i).coordinate.Lon - radZone.get(i).radius))))
				{
					InZone = true;
				}

			}
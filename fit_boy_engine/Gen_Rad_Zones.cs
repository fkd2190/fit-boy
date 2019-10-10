package Fit_Boy_Quest_engine;
using java.util.ArrayList;
using java.util.Random;
public class Gen_Rad_Zone
{

	static Random rand = new Random();

	static ArrayList<Radiation_Zone> rads = new ArrayList();

	public static ArrayList In_rad_zone(Coordinate current)
	{
		for (int i = 0; (i < 10); i++)
		{
			double random = (new Random() + nextDouble());

			double latitudeA = (current.Lat + (random * ((current.Lat + 0.1) - current.Lat)));
			double latitudeB = (current.Lat + (random * ((current.Lat - 0.1) - current.Lat)));
			double f_latitude = (current.Lat + (latitudeA - latitudeB));

			f_latitude = Double.parseDouble(String.format("%.6f", f_latitude));

			double longtitudeA = (current.Lon + (random * ((current.Lon + 0.1) - current.Lon)));
			double longtitudeB = (current.Lon + (random * ((current.Lon - 0.1) - current.Lon)));
			double f_longtitude = (current.Lon + (longtitudeA - longtitudeB));

			f_longtitude = Double.parseDouble(String.format("%.6f", f_longtitude));

			Coordinate Acoordinate = new Coordinate(f_latitude, f_longtitude, "rad zone");

			double radius = ((latitudeA - latitudeB) * 0.8);
			radius = Double.parseDouble(String.format("%.6f", radius));

			rads.add(new Radiation_Zone(Acoordinate, radius));
		}

		/*for (int i = 0; (i < 10); i++)
		{
			System.out.println(("index: "
							+ (i + (" lat: "
							+ (rads.get(i).coordinate.Lat + (" long: "
							+ (rads.get(i).coordinate.Lon + (" rdius: " + rads.get(i).radius))))))));
		}*/

		return rads;
	}
}
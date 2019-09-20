package Fit_Boy_Quest_engine;
public class Compair_coordinates
{

	public static bool Are_We_There_Yet(Coordinate current, Coordinate dest)
	{
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
}


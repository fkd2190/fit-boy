using System;
public class Gen_xp
{

	static int xp;

	static Random rand = new Random();

	public static int Xp_gen()
	{
		xp = (rand.Next(((60 - 10)
						+ 1)) + 10);
		return xp;
	}
}

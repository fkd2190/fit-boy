using java.util.Random;
package Fit_Boy_Quest_engine;

public class Gen_xp
{

	static int xp;

	static Random rand = new Random();

	public static int Xp_gen()
	{
		xp = (rand.nextInt(((60 - 10)
						+ 1)) + 10);
		return xp;
	}
}

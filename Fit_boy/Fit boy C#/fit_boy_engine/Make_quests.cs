
package Fit_Boy_Quest_engine;
using java.util.ArrayList;
using java.util.Random;
public class Make_quests
{

	public static ArrayList Gen_coordinates()
	{
		ArrayList<Coordinate> AllCo = new ArrayList();
		// 12
		AllCo.add(new Coordinate(-36.840939, 174.763396, "Maritime Museum"));
		AllCo.add(new Coordinate(-36.850636, 174.767918, "Albert park"));
		AllCo.add(new Coordinate(-36.846896, 174.754028, "Victoria park"));
		AllCo.add(new Coordinate(-36.85289, 174.767382, "Auckland uni"));
		AllCo.add(new Coordinate(-36.852835, 174.767313, "AUT"));
		AllCo.add(new Coordinate(-36.851967, 174.763049, "Aotea square"));
		AllCo.add(new Coordinate(-36.848043, 174.762203, "Sky tower"));
		AllCo.add(new Coordinate(-36.849139, 174.762352, "Sky city"));
		AllCo.add(new Coordinate(-36.859973, 174.777731, "War memorial museum"));
		AllCo.add(new Coordinate(-36.877289, 174.76419, "MT Eden summit"));
		AllCo.add(new Coordinate(-36.863184, 174.71948, "Zoo"));
		AllCo.add(new Coordinate(-36.847721, 174.831479, "Mission bay"));
		return AllCo;
	}

	public static ArrayList Gen_quest_info()
	{
		ArrayList<Quest_info> Allinfo = new ArrayList();
		//  7
		Allinfo.add(new Quest_info("B.O.S Supply Run", "The Brothe hood of steel needs your help, paladin Danse is stranded at the location on your map and n" +
				"eeds medical supplies. You  will deliver the supplies to him."));
		Allinfo.add(new Quest_info("Legion Supply Run", "Casars Legion needs your help, Caius Drusus is stranded at the location on your map and needs some fo" +
				"od for his Centuria:. You  will deliver the supplies to him."));
		Allinfo.add(new Quest_info("NCR Supply Run", "The new California Republic needs your help, James Hsu is surrounded at the location on your map and " +
				"needs some ammo to fuel the fight. You  will deliver the supplies to him."));
		Allinfo.add(new Quest_info("Vagas Run", "Yes MAN needs you to go to the location on your map and collect some caps so you  can lay down some b" +
				"ets for him in the strip."));
		Allinfo.add(new Quest_info("Rail Road Synth Escort", "The new Rail Road needs your help, Deacon has a synth at the location on your map and needs you to es" +
				"cort it back to HQ. Go meat Deacon."));
		Allinfo.add(new Quest_info("Minute Men Settlement Aid", "The Minute Men needs your help, Preston Garvey has a settlement that needs help go to the location on" +
				" your map."));
		Allinfo.add(new Quest_info("Fighting the good Fight", "Three dog needs your help spreading the word to the people. Head to the location on your map and talk" +
				" to the people."));
		return Allinfo;
	}

	public static Quest Gen_Quest()
	{
		Random rand = new Random();
		ArrayList<Coordinate> AllCo = Make_quests.Gen_coordinates();
		ArrayList<Quest_info> Allinfo = Make_quests.Gen_quest_info();
		int Xp = Gen_xp.Xp_gen();
		int Level = Gen_level.Level_gen();
		int coord = rand.nextInt(((11 - 0)+ 1));
		int info = rand.nextInt(((6 - 0)+ 1));

		Quest AQuest = new Quest(Allinfo.get(info), Xp, Level, AllCo.get(coord), false);
		
		System.out.println(("Title      : " + AQuest.info.Title));
		System.out.println(("Descr      : " + AQuest.info.Desc));
		System.out.println(("Level      : " + AQuest.Level));
		System.out.println(("Xp         : " + AQuest.Xp_reward));
		System.out.println(("Active     : " + AQuest.Active));
		System.out.println(("latitude   : " + AQuest.Stop_co.Lat));
		System.out.println(("longtitude : " + AQuest.Stop_co.Lon));
		System.out.println(("Lacation   : " + AQuest.Stop_co.Name));
		return AQuest;
	}
}
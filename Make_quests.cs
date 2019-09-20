package Fit_Boy_Quest_engine;
using java.util.ArrayList;
using java.util.Random;
public class Make_quests
{

	public static ArrayList Gen_coordinates()
	{
		ArrayList<Coordinate> AllCo = new ArrayList();
		//  61
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
		AllCo.add(new Coordinate(-36.855902, 174.752436, "Western Park"));
		AllCo.add(new Coordinate(-36.863351, 174.786687, "Ayr Reserve"));
		AllCo.add(new Coordinate(-36.849169, 174.78651, "Rose Garden"));
		AllCo.add(new Coordinate(-36.850163, 174.73039, "Bayfield Park"));
		AllCo.add(new Coordinate(-36.850984, 174.725429, "Cox\'s Bay"));
		AllCo.add(new Coordinate(-36.852183, 174.731331, "Hukanui Reserve"));
		AllCo.add(new Coordinate(-36.859438, 174.738258, "Grey Lynn Park"));
		AllCo.add(new Coordinate(-36.867102, 174.727118, "MOTAT"));
		AllCo.add(new Coordinate(-36.845839, 174.766957, "KFC"));
		AllCo.add(new Coordinate(-36.853581, 174.767033, "Elixir of life, the watering whole, the pub"));
		AllCo.add(new Coordinate(-36.848393, 174.766081, "The other pub"));
		AllCo.add(new Coordinate(-36.844423, 174.765538, "Commonwealth Vault"));
		// 
		AllCo.add(new Coordinate(-36.842922, 174.766973, "The Boat to Point lookout"));
		AllCo.add(new Coordinate(-36.824882, 174.802672, "DevonPort Museum"));
		AllCo.add(new Coordinate(-36.826337, 174.806911, "Sports Field"));
		AllCo.add(new Coordinate(-36.827269, 174.81195, "North Head Reserve"));
		AllCo.add(new Coordinate(-36.812911, 174.770934, "Shole Bay"));
		AllCo.add(new Coordinate(-36.814253, 174.787986, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.803353, 174.779583, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.801788, 174.782088, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.803597, 174.782349, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.804491, 174.782025, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.805412, 174.780956, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.806537, 174.77986, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.808072, 174.780184, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.812434, 174.781121, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.812514, 174.781206, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.813437, 174.781962, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.813974, 174.783782, "Green route Entry"));
		AllCo.add(new Coordinate(-36.81333, 174.786679, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.812738, 174.788248, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.814253, 174.78797, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.817944, 174.794654, "Green Route Entry"));
		AllCo.add(new Coordinate(-36.826512, 174.792007, "Melrose Reserve"));
		AllCo.add(new Coordinate(-36.826719, 174.798605, "Mount Victoria Summit"));
		AllCo.add(new Coordinate(-36.784667, 174.773212, "Elephant Wrestler"));
		AllCo.add(new Coordinate(-36.866675, 174.729754, "Wester Springs Park"));
		AllCo.add(new Coordinate(-36.864267, 174.726744, "Western Springs Speedway"));
		AllCo.add(new Coordinate(-36.858945, 174.718056, "Joggers Bush Reserve"));
		AllCo.add(new Coordinate(-36.857447, 174.710673, "Meola Reef Reserve"));
		AllCo.add(new Coordinate(-36.858325, 174.776703, "Robert Burns Statue"));
		AllCo.add(new Coordinate(-36.859987, 174.774084, "Domain Gardens"));
		AllCo.add(new Coordinate(-36.857843, 174.775047, "Domain Garden"));
		AllCo.add(new Coordinate(-36.855315, 174.76121, "Myers Park"));
		AllCo.add(new Coordinate(-36.857504, 174.758785, "Callander Girls"));
		AllCo.add(new Coordinate(-36.857938, 174.757419, "Pre_War Tech"));
		AllCo.add(new Coordinate(-36.872078, 174.763004, "Playground"));
		AllCo.add(new Coordinate(-36.869165, 174.76495, "Good home"));
		AllCo.add(new Coordinate(-36.866941, 174.765678, "End of the road"));
		return AllCo;
	}

	public static ArrayList Gen_quest_info()
	{
		ArrayList<Quest_info> Allinfo = new ArrayList();
		//  16
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
		Allinfo.add(new Quest_info("Freedom calling", "Casars Legion has captured and enslaved some Minutemen settlers, raid their camp at the location on y" +
				"our map to set the slaves free."));
		Allinfo.add(new Quest_info("paradise falls slave run", "The supply of slaves at Paradise falls has run low, go to the location on your pip-boy to collect som" +
				"e more unsuspecting settlers."));
		Allinfo.add(new Quest_info("Knowledge is key", "The Brotherhood wants you to search this location for pre-war technical documents so they can learn m" +
				"ore."));
		Allinfo.add(new Quest_info("Settlement rescue", "One of the minute men settlements is in trouble, go and save them from impending doom."));
		Allinfo.add(new Quest_info("Cleansing the commonwealth", "The B.O.S has found a group of Super Mutants at this location, go and eradicate them."));
		Allinfo.add(new Quest_info("Cleansing the commonwealth", "The B.O.S has found a group of ferral Ghouls at this location, go and eradicate them"));
		Allinfo.add(new Quest_info("Derailing the institute", "The rail road has found an institute outpost, travel to it and dissable it."));
		Allinfo.add(new Quest_info("Tech scavange", "Rumours of some pre-war tech have spread from this location, go find the tech and sell it to the high" +
				"est bidder."));
		Allinfo.add(new Quest_info("Vault eploration", "A new lault signal is being picked up on your pip-boy, go investigate."));
		return Allinfo;
	}

	public static Quest Gen_Quest()
	{
		Random rand = new Random();
		ArrayList<Coordinate> AllCo = Make_quests.Gen_coordinates();
		ArrayList<Quest_info> Allinfo = Make_quests.Gen_quest_info();
		int Xp = Gen_xp.Xp_gen();
		int Level = Gen_level.Level_gen();
		int coord = rand.nextInt((((AllCo.size() - 1)
						- 0)
						+ 1));
		int info = rand.nextInt((((Allinfo.size() - 1)
						- 0)
						+ 1));
		Quest AQuest = new Quest(Allinfo.get(info), Xp, Level, AllCo.get(coord), false);
		Coordinate Test_dest = new Coordinate(-36.866941, 174.765679, null);
		Coordinate Test_cur = new Coordinate(-36.866948, 174.765688, null);
		bool there = Compair_coordinates.Are_We_There_Yet(Test_dest, Test_cur);
		//  AllQuests.add(new Quest("B.O.S Supply Run","The Brothe hood of steel needs your help, paladin Danse is stranded at the location on your map and needs medical supplies. You  will deliver the supplies to him." ,2 , 2, test, false));
		System.out.println(("Title      : " + AQuest.info.Title));
		System.out.println(("Descr      : " + AQuest.info.Desc));
		System.out.println(("Level      : " + AQuest.Level));
		System.out.println(("Xp         : " + AQuest.Xp_reward));
		System.out.println(("Active     : " + AQuest.Active));
		System.out.println(("latitude   : " + AQuest.Stop_co.Lat));
		System.out.println(("longtitude : " + AQuest.Stop_co.Lon));
		System.out.println(("Lacation   : " + AQuest.Stop_co.Name));
		System.out.println(("are we there yet? " + there));
		return AQuest;
	}
}

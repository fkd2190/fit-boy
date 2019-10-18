using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Make_quests
{
    private System.Random rand;

    public Make_quests()
    {
        rand = new System.Random();
    }

    public static ArrayList Gen_GPSCoordinates()
	{
		ArrayList AllCo = new ArrayList();
		// 12
		AllCo.Add(new GPSCoordinate(-36.840939, 174.763396, "Maritime Museum"));
		AllCo.Add(new GPSCoordinate(-36.850636, 174.767918, "Albert park"));
		AllCo.Add(new GPSCoordinate(-36.846896, 174.754028, "Victoria park"));
		AllCo.Add(new GPSCoordinate(-36.85289, 174.767382, "Auckland uni"));
		AllCo.Add(new GPSCoordinate(-36.852835, 174.767313, "AUT"));
		AllCo.Add(new GPSCoordinate(-36.851967, 174.763049, "Aotea square"));
		AllCo.Add(new GPSCoordinate(-36.848043, 174.762203, "Sky tower"));
		AllCo.Add(new GPSCoordinate(-36.849139, 174.762352, "Sky city"));
		AllCo.Add(new GPSCoordinate(-36.859973, 174.777731, "War memorial museum"));
		AllCo.Add(new GPSCoordinate(-36.877289, 174.76419, "MT Eden summit"));
		AllCo.Add(new GPSCoordinate(-36.863184, 174.71948, "Zoo"));
		AllCo.Add(new GPSCoordinate(-36.847721, 174.831479, "Mission bay"));
		return AllCo;
	}

	public static ArrayList Gen_quest_info()
	{
		ArrayList Allinfo = new ArrayList();
		//  7
		Allinfo.Add(new Quest_info("B.O.S Supply Run", "The Brothe hood of steel needs your help; paladin Danse is stranded at the location on your map and n" +
				"eeds medical supplies. You  will deliver the supplies to him."));
		Allinfo.Add(new Quest_info("Legion Supply Run", "Casars Legion needs your help; Caius Drusus is stranded at the location on your map and needs some fo" +
				"od for his Centuria:. You  will deliver the supplies to him."));
		Allinfo.Add(new Quest_info("NCR Supply Run", "The new California Republic needs your help; James Hsu is surrounded at the location on your map and " +
				"needs some ammo to fuel the fight. You  will deliver the supplies to him."));
		Allinfo.Add(new Quest_info("Vagas Run", "Yes MAN needs you to go to the location on your map and collect some caps so you  can lay down some b" +
				"ets for him in the strip."));
		Allinfo.Add(new Quest_info("Rail Road Synth Escort", "The new Rail Road needs your help; Deacon has a synth at the location on your map and needs you to es" +
				"cort it back to HQ. Go meat Deacon."));
		Allinfo.Add(new Quest_info("Minute Men Settlement Aid", "The Minute Men needs your help; Preston Garvey has a settlement that needs help go to the location on" +
				" your map."));
		Allinfo.Add(new Quest_info("Fighting the good Fight", "Three dog needs your help spreading the word to the people. Head to the location on your map and talk" +
				" to the people."));
		return Allinfo;
	}

	public Quest Gen_Quest()
	{
		ArrayList AllCo = Make_quests.Gen_GPSCoordinates();
		ArrayList Allinfo = Make_quests.Gen_quest_info();
		int Xp = Gen_xp.Xp_gen();
		int Level = Gen_level.Level_gen();
		int coord = rand.Next(((11 - 0)+ 1));
		int info = rand.Next(((6 - 0)+ 1));
        Quest_info qi = (Quest_info)Allinfo[info];
        GPSCoordinate gps = (GPSCoordinate)AllCo[coord];
        Quest AQuest = new Quest(qi, Xp, Level, null, gps);
        return AQuest;
	}
}
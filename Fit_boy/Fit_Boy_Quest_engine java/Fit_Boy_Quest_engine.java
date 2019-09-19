package Fit_Boy_Quest_engine;

public class Fit_Boy_Quest_engine 
{
    
    

    public static void main(String[] args) 
    {
        Make_quests.Gen_Quest();
        //Quest Aquest = Gen_Quest();
       // System.out.println(Aquest.Title);
       // System.out.println(Aquest.Description);
       // System.out.println("latitude: " + Aquest.Stop_co.Lat + " Longtitude: " + Aquest.Stop_co.Lon + " Name: " + Aquest.Stop_co.Name);
       // System.out.println("XP: " + Aquest.Xp_reward);
        //System.out.println("Level: " + Aquest.Level);
        
        
    }
    
    /*public static Quest Gen_Quest()
        {
            String Title = null;
            String Desc = null;
            // public static String Start_date;
            // public static String Exp_date;
            // public static int Distance;
            int Xp;
            int Level;
            //Coordinate Start_co;
            Coordinate Stop_co;
            
            Title = Quest_title.get_title();
            System.out.println("title: " +Title);
            Desc = Quest_desc.Get_desc();
            System.out.println("description: " + Desc);
            Stop_co = Quest_destination.get_dest();
            System.out.println("latitude: " + Stop_co.Lat + "longtitude: " + Stop_co.Lon);
            Xp = Gen_xp.Xp_gen();
            System.out.println("XP: " + Xp);
            Level = Gen_level.Level_gen();
            System.out.println("level: " + Level);
            Quest_info info = Quest_info
            
            Quest Aquest = new Quest(info, Xp, Level, Stop_co, false);
            
            return Aquest;
            
        }*/
}

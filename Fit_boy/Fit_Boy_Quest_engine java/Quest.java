
package Fit_Boy_Quest_engine;

public class Quest 
{
    Quest_info info;
   //String Start_date;
    //String Expiry_date;
   //int Distance;
    int Xp_reward;
    //Coordinate Start_co;
    Coordinate Stop_co;
    int Level;
    boolean Active;
    
    public Quest(Quest_info info, int xp_reward, int level, Coordinate stop_co, boolean active)
    {
        this.info = info;
       // this.Start_date = start_date;
       // this.Expiry_date = expiry_date;
       // this.Distance = distance;
        this.Xp_reward = xp_reward;
        this.Level = level;
       // this.Start_co = start_co;
        this.Stop_co = stop_co;
        this.Active = active;
    }
    
}

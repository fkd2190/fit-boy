
package Fit_Boy_Quest_engine;

import java.util.Random;

public class Gen_level 
{
    static int level;
    
    static Random rand = new Random();
    
    public static int Level_gen()
    {
        int scale = rand.nextInt((100-1)+1)+1;
        
        if(scale > 0 && scale < 51)
        {
            level = rand.nextInt((10-1)+1)+1;
        }
        else if(scale > 50 && scale > 61)
        {
            level = rand.nextInt((30-10)+1)+10;
        }
        else if(scale > 60 && scale > 71)
        {
            level = rand.nextInt((40-30)+1)+30;
        }
        else if(scale > 70 && scale > 81)
        {
            level = rand.nextInt((80-40)+1)+40;
        }
        else if(scale > 80 && scale > 100)
        {
            level = rand.nextInt((100-80)+1)+80;
        }
        return level;
    }   
}

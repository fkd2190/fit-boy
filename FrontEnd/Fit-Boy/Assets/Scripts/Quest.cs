public class Quest
{

	public Quest_info info { get; set; }
	public int Xp_reward { get; set; }
    public GPSCoordinate Stop_co { get; set; }
    public GPSCoordinate Start_co { get; set; }
    public int Level;
    public bool crossedRadZone;

	public Quest(Quest_info info, int xp_reward, int level, GPSCoordinate Start_co, GPSCoordinate stop_co)
	{
		this.info = info;
		this.Xp_reward = xp_reward;
		this.Level = level;
        this.Start_co = Start_co;
		this.Stop_co = stop_co;
        crossedRadZone = false;
	}
}

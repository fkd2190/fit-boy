package Fit_Boy_Quest_engine;
public class Quest
{

	Quest_info info;
	int Xp_reward;
	Coordinate Stop_co;
	int Level;
	bool Active;

	public Quest(Quest_info info, int xp_reward, int level, Coordinate stop_co, bool active)
	{
		this.info = this.info;
		this.Xp_reward = xp_reward;
		this.Level = level;
		this.Stop_co = stop_co;
		this.Active = active;
	}
}

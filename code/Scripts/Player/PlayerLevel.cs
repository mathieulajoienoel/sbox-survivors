using System;

public sealed class PlayerLevel : Component {
  [RequireComponent] private PlayerMaster master { get; set; }

	protected override void OnEnabled()
	{
    GameMaster.Instance.ExperienceGainEvent += OnExperienceGain;
	}
  protected override void OnDisabled()
	{
    GameMaster.Instance.ExperienceGainEvent -= OnExperienceGain;
	}

  private void OnExperienceGain(float value){
    CheckLevelUp();
  }

  public void CheckLevelUp(){
    PlayerStats playerStats = master.Stats;
    if(playerStats.Experience < playerStats.ExperienceNeeded) return;

    playerStats.Level += 1;
    playerStats.Experience = 0;
    playerStats.ExperienceNeeded *= (float)Math.Floor(playerStats.ExperienceNeeded / 8); //(float)GameMaster.Instance.LevelData.DifficultyMultiplier;
    Log.Info("Level up to level " + playerStats.Level);
    Log.Info("Needs " + playerStats.ExperienceNeeded + " xp for next level");
    master.CallEventLevelUp(playerStats.Level);
  }
}
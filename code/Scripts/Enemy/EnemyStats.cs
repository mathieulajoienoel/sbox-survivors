using System;

public sealed class EnemyStats : EntityStats
{
	[RequireComponent] private EnemyMaster master { get; set; }
	[Property] public float ExperienceDrop { get; set; } = 1f;

	// @@TODO apply enemy type stats

  private void Prepare(){
		float difficulty = (float)GameMaster.Instance.LevelData.DifficultyMultiplier;
		ExperienceDrop = (float)Math.Floor(ExperienceDrop * difficulty);
		MaxHealth = (float)Math.Floor(MaxHealth * difficulty);
		MoveSpeed = (float)Math.Floor(MoveSpeed * difficulty);
  }

	protected override void OnEnabled(){
		base.OnEnabled();
		master.EventPrepare += Prepare;
	}
	protected override void OnDisabled(){
		base.OnDisabled();
		master.EventPrepare -= Prepare;
	}
	public override void Reset(){
		Alive = true;
		ExperienceDrop = 1f;
  }
}

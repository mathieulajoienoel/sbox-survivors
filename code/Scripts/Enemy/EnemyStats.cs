using System;

public sealed class EnemyStats : EntityStats
{
	[Property] public float ExperienceDrop { get; set; } = 1f;

	protected override void OnAwake()
	{
		float difficulty = (float)GameMaster.Instance.LevelData.DifficultyMultiplier;
		ExperienceDrop = (float)Math.Floor(ExperienceDrop * difficulty);
		MaxHealth = (float)Math.Floor(MaxHealth * difficulty);
		MoveSpeed = (float)Math.Floor(MoveSpeed * difficulty);
		base.OnAwake();
	}
}

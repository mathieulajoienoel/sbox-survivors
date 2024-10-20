using System;

public sealed class EnemyStats : EntityStats
{
	[RequireComponent] private EnemyMaster master { get; set; }
	[Property] public float ExperienceDrop { get; set; } = 1f;

	// @@TODO apply enemy type stats

  public void Prepare(){
		float difficulty = (float)GameMaster.Instance.LevelData.DifficultyMultiplier;
		ExperienceDrop = (float)Math.Floor(ExperienceDrop * difficulty);
		MaxHealth = (float)Math.Floor(MaxHealth * difficulty);
		MoveSpeed = (float)Math.Floor(MoveSpeed * difficulty);
  }

	protected override void OnAwake()
	{
		base.OnAwake();
		master.EventPrepare += Prepare;
	}
	protected override void OnDestroy()
	{
		base.OnDestroy();
		master.EventPrepare -= Prepare;
	}

	//protected override void OnEnabled(){
	//	Log.Info("OnEnabled");
	//
	//	base.OnEnabled();
	//}
	//protected override void OnDisabled(){
	//	base.OnDisabled();
	//	master.EventPrepare -= Prepare;
	//}
	public override void Reset(){
		Alive = true;
		ExperienceDrop = 1f;
  }

	protected override void OnAttributeChange(Dictionary<string, float> AttributesValues){
		// @@TODO change attributes ?

		master.CallEventAttributesChanged();
	}
}

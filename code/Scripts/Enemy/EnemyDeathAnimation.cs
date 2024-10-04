
public sealed class EnemyDeathAnimation : Component {
  [RequireComponent] public EnemyMaster master { get; set; }

  [Property] public float TimeToWait { get; set; } = -1f;
  [Property] public float DestroyAfter { get; set; } = 2f;
  [Property] public float Speed { get; set; } = 20f;
  private float WaitTo = 0.0f;
  private float DestroyAt = 0.0f;

  private bool Alive = true;

  [Property] public string EnemyPool { get; set; } = "EnemyPool";

	protected override void OnEnabled()
	{
    Alive = true;
    WaitTo = 0.0f;
    DestroyAt = 0.0f;
    WorldScale = 1f;

    master.EventDeath += OnDeath;
	}
  protected override void OnDisabled(){
    master.EventDeath -= OnDeath;
  }

  private void OnDeath(){
    Alive = false;
    WaitTo = Time.Now + TimeToWait;
    DestroyAt = WaitTo + DestroyAfter;
  }

  protected override void OnUpdate(){
    if(Alive) return;

    if(Time.Now > WaitTo) {
      WorldScale = WorldScale.LerpTo(WorldScale - WorldScale * 0.1f, Time.Delta * Speed);
    }

    if(Time.Now > DestroyAt){
      //GameObject.Destroy();
      ObjectPool.Instance.ReturnToPool(EnemyPool, GameObject);
    }
  }
}
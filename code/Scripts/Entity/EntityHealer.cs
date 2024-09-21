public abstract class EntityHealer<T> : Component
where T : EntityMaster
{
  private T master { get; set; }
  private float NextHeal = 0f;
  private float HealthRegenerationRate = 0f;
  private float MaxHealth = 0f;

  protected override void OnEnabled(){
    master = Components.Get<T>();
    HealthRegenerationRate = master.Stats.HealthRegenerationRate;
    MaxHealth = master.Stats.MaxHealth;
    // @@TODO update stats OnStatsUpdate
  }
	protected override void OnFixedUpdate()
	{
    if(HealthRegenerationRate > 0 && (NextHeal == 0f || NextHeal < Time.Now)){
      if(master.Health.CurrentHealth < MaxHealth) RegenerateHealth();
      NextHeal = Time.Now + 1f;
    }
  }

  private void RegenerateHealth(){
    master.CallEventReceiveDamage(new DamageInfo(HealthRegenerationRate, GameObject, GameObject));
  }
}
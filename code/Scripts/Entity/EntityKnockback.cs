public abstract class EntityKnockback<T> : Component  where T : EntityMaster {
  [Property] public bool IsKnockedback { get; set; } = false;
  public float ReleaseTime { get; set; } = 0f;
  private T master;

  protected override void OnEnabled(){
    IsKnockedback = false;
    master = Components.GetInParentOrSelf<T>();
    master.EventKnockback += OnKnockback;
  }
  protected override void OnDisabled()
	{
    master.EventKnockback -= OnKnockback;
	}

  public void OnKnockback (float knockbackDuration = 1f){
    if(!master.Stats.Alive && !master.Health.CanBeDamaged) return;
    IsKnockedback = true;
    ReleaseTime = Time.Now + knockbackDuration;
  }

  protected override void OnUpdate (){
    if(IsKnockedback && Time.Now > ReleaseTime) {
      IsKnockedback = false;
      master.CallEventKnockbackRelease();
    }
  }

}
public abstract class EntityKnockback<T> : Component  where T : EntityMaster {
  public bool IsKnockedback = false;
  public float ReleaseTime;
  private T master;

  protected override void OnEnabled(){
    master = Components.GetInParentOrSelf<T>();
    master.EventKnockback += OnKnockback;
  }
  protected override void OnDisabled()
	{
    master.EventKnockback -= OnKnockback;
	}

  public void OnKnockback (float knockbackDuration = 1f){
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
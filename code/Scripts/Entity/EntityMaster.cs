public abstract class EntityMaster : Component
{
  public IEntityStats Stats { get; set; }
  public EntityCharacter Character { get; set; }
  public IEntityHealth Health { get; set; }

  // Events & Delegates
  public delegate void HealthEventHandler(float healthChange);
	public event HealthEventHandler EventHealthChanged;
  public delegate void DeathEventHandler();
  public event DeathEventHandler EventDeath;
  public delegate void DealDamageEventHandler(DamageInfo damage);
  public event DealDamageEventHandler EventDealDamage;
  public delegate void ReceiveDamageEventHandler(DamageInfo damage);
  public event ReceiveDamageEventHandler EventReceiveDamage;
  public void CallEventHealthChanged(float healthChange){
		EventHealthChanged?.Invoke( healthChange );
	}
  public void CallEventDeath(){
		EventDeath?.Invoke();
	}
  public void CallEventDealDamage(DamageInfo damage){
		EventDealDamage?.Invoke(damage);
	}
  public void CallEventReceiveDamage(DamageInfo damage){
		EventReceiveDamage?.Invoke(damage);
	}

  protected override void OnAwake(){
    Character = Components.GetInChildrenOrSelf<EntityCharacter>();
    Health = Components.Get<IEntityHealth>();
    Stats = Components.Get<IEntityStats>();
  }

}
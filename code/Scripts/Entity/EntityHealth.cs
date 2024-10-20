public abstract class EntityHealth<T> : Component, IEntityHealth, IExternalAttributes where T : EntityMaster
{
  protected T master;

  public float CurrentHealth { get; set; }
  [Property] public bool CanBeDamaged { get; set; } = true;
  private float NextCanBeDamaged = 0f;
  private float InvincibilityTime = 0f;
  public string DamagePopupPool { get; set; } = "DamagePopup";

  protected override void OnEnabled(){
    master = Components.Get<T>();
    // @@TODO On stats update

    master.EventReceiveDamage += OnReceiveDamage;
    master.EventHealthChanged += OnHealthChanged;
    master.EventDeath += OnDeath;
    master.EventStart += Start;
    master.EventAttributesChanged += OnAttributesChanged;
  }

	protected override void OnDisabled()
	{
		base.OnDisabled();
    master.EventReceiveDamage -= OnReceiveDamage;
    master.EventHealthChanged -= OnHealthChanged;
    master.EventDeath -= OnDeath;
    master.EventStart -= Start;
    master.EventAttributesChanged -= OnAttributesChanged;
	}

  public virtual void OnAttributesChanged(){
    CurrentHealth = master.Stats.MaxHealth;
    InvincibilityTime = master.Stats.InvincibilityTime;
  }

  private void Start(){
    OnAttributesChanged();
    //float maxHealth = master.Stats.MaxHealth;
    CurrentHealth = master.Stats.MaxHealth;
    InvincibilityTime = master.Stats.InvincibilityTime;
  }

	public void ChangeHealth(float value){
    CurrentHealth += value;
    master.CallEventUpdateHealthDisplay(CurrentHealth, master.Stats.MaxHealth);
  }

  public void OnHealthChanged(float value){
    if(!master.Stats.Alive) return;
    ChangeHealth(value);
    DisplayDamageReceived(value);
    CheckDeath();
  }
  public void OnReceiveDamage(DamageInfo damage){
    if(CanBeDamaged) master.CallEventHealthChanged(damage.Damage);
  }

	protected override void OnFixedUpdate()
	{
    if(!CanBeDamaged && (NextCanBeDamaged == 0f || NextCanBeDamaged < Time.Now)){
      CanBeDamaged = true;
      NextCanBeDamaged = Time.Now + InvincibilityTime;
    }
	}

	private void DisplayDamageReceived(float damage){
    Vector3 position = master.GameObject.WorldPosition;
    position.z = 15;
    position += new Vector3(GameMaster.Instance.Rand(-20,20),GameMaster.Instance.Rand(-20,20), 0);

    GameObject damagePopup = ObjectPool.Instance.GetObjectFromPool(DamagePopupPool);
    if(damagePopup == null) return;
    damagePopup.WorldPosition = position;
    //GameObject damagePopup = GameMaster.Instance.DamagePopupPrefab.Clone(position);
    damagePopup.Components.Get<DamagePopup>(true).Display(damage);
  }

  public virtual void CheckDeath(){
    if(CurrentHealth < 1 && master.Stats.Alive){
      master.CallEventDeath();
    }
  }

  public virtual void OnDeath(){
    if(!master.Stats.Alive) return;
    master.Stats.Alive = false;
    //FadeOutAfterTime comp = Components.Create<FadeOutAfterTime>(false);
    //comp.TimeToWait = -1f;
    //comp.DestroyAfter = 2f;
    //comp.Speed = 20f;
    //comp.Enabled = true;
  }
}
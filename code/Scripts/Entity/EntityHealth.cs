public abstract class EntityHealth<T> : Component, IEntityHealth where T : EntityMaster
{
  private T master;
  private TextRenderer HealthPanel { get; set; }
  private float HealthAlertLimit { get; set; }
  public float CurrentHealth { get; set; }
  protected override void OnEnabled(){
    master = Components.Get<T>();

    CurrentHealth = master.Stats.MaxHealth;
    HealthAlertLimit = CurrentHealth * 20 / 100;
    HealthPanel = Components.GetAll<TextRenderer>().FirstOrDefault(x => x.GameObject.Name == "HealthPanel");
    UpdateHealthDisplay();

    master.EventReceiveDamage += OnReceiveDamage;
    master.EventHealthChanged += OnHealthChanged;
    master.EventDeath += OnDeath;
  }

	protected override void OnDisabled()
	{
		base.OnDisabled();
    master.EventHealthChanged -= OnHealthChanged;
    master.EventDeath -= OnDeath;
	}

	public void ChangeHealth(float value){
    CurrentHealth += value;
    UpdateHealthDisplay();
  }

  public void UpdateHealthDisplay(){
    HealthPanel.Text = CurrentHealth.ToString();
    if(CurrentHealth < HealthAlertLimit){
      HealthPanel.Color = "#FF0000";
    } else {
      HealthPanel.Color = "#000000";
    }
  }
  public void OnHealthChanged(float value){
    if(!master.Stats.Alive) return;
    ChangeHealth(value);
    DisplayDamageReceived(value);
    CheckDeath();
  }
  public void OnReceiveDamage(DamageInfo damage){
    master.CallEventHealthChanged(damage.Damage);
  }

  private void DisplayDamageReceived(float damage){
    Vector3 position = master.GameObject.Transform.Position;
    position.z = 15;
    GameObject damagePopup = GameMaster.Instance.DamagePopupPrefab.Clone(position);
    damagePopup.Components.Get<DamagePopup>().Display(damage);
  }

  public void CheckDeath(){
    if(CurrentHealth < 1 && master.Stats.Alive){
      master.CallEventDeath();
    }
  }

  public void OnDeath(){
    if(!master.Stats.Alive) return;
    master.Stats.Alive = false;
    FadeOutAfterTime comp = Components.Create<FadeOutAfterTime>(false);
    comp.TimeToWait = -1f;
    comp.DestroyAfter = 2f;
    comp.Speed = 20f;
    comp.Enabled = true;
  }
}
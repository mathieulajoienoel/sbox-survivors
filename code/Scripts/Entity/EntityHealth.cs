using System;

public abstract class EntityHealth<T> : Component, IEntityHealth where T : EntityMaster
{
  private T master;

  public float CurrentHealth { get; set; }
  protected override void OnEnabled(){
    master = Components.Get<T>();
    float maxHealth = master.Stats.MaxHealth;
    CurrentHealth = master.Stats.MaxHealth;

    master.EventReceiveDamage += OnReceiveDamage;
    master.EventHealthChanged += OnHealthChanged;
    master.EventDeath += OnDeath;
  }

	protected override void OnDisabled()
	{
		base.OnDisabled();
    master.EventReceiveDamage -= OnReceiveDamage;
    master.EventHealthChanged -= OnHealthChanged;
    master.EventDeath -= OnDeath;
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
    master.CallEventHealthChanged(damage.Damage);
  }

  private void DisplayDamageReceived(float damage){
    Vector3 position = master.GameObject.Transform.Position;
    position.z = 15;
    position += new Vector3(GameMaster.Instance.Rand(-20,20),GameMaster.Instance.Rand(-20,20), 0);
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
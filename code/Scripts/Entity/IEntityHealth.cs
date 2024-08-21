public interface IEntityHealth {
  float CurrentHealth { get; set; }
  public abstract void ChangeHealth(float value);
  public abstract void UpdateHealthDisplay();
  public abstract void OnHealthChanged(float value);
  public abstract void OnReceiveDamage(DamageInfo damage);
  public abstract void CheckDeath();
  public abstract void OnDeath();
}
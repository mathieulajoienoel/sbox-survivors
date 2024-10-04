public enum HolsterType { FixedWeaponHolster, AimedWeaponHolster }

public abstract class WeaponBase<T, Y> : Component, Component.ITriggerListener, IHolsteredWeapon
where T : EntityMaster
where Y : EntityMaster
{
  protected abstract float Damage { get; set; }
  protected abstract float Cooldown { get; set; }
  protected abstract Vector3 Range { get; set; }
  protected abstract Vector3 Size { get; set; }
  protected abstract Vector3 Position { get; set; }
  protected abstract Angles StartRotation { get; set; }
  protected abstract int Speed { get; set; }
  protected abstract int SubWeaponSpeed { get; set; }

  protected abstract int MaxTargets { get; set; }

  protected abstract string OpponentTag { get; set; }

  protected abstract float Knockback { get; set; }
  protected abstract float KnockbackDuration { get; set; }

  protected float NextHit { get; set; } = 0f;
  protected HashSet<Collider> Colliders = new HashSet<Collider>();
  protected T master;

  [Property] public virtual HolsterType WeaponHolster { get; set; } = HolsterType.FixedWeaponHolster;

  protected override void OnEnabled(){
    master = Components.GetInAncestorsOrSelf<T>();

    ApplyStartingPosition();
    ApplyStartingRotation();
    ApplySpeed();
    ApplyRange();
    ApplySize();
  }
  protected virtual void ApplyStartingPosition(){
    LocalPosition = Position;
  }
  protected virtual void ApplyStartingRotation(){
    LocalRotation = Rotation.From(StartRotation);
  }
  protected virtual void ApplyRange(){
    //LocalScale = Range;
  }
  protected virtual void ApplySpeed(){
  }

  protected virtual void ApplySize(){
    LocalScale = Size;
  }

  public virtual void OnTriggerEnter( Collider other )
  {
    Colliders.Add( other );
  }
  public void OnTriggerExit( Collider other )
  {
    Colliders.Remove( other );
  }

	protected override void OnFixedUpdate()
  {
    if(NextHit == 0f || NextHit < Time.Now){
      int targets = 0;
      foreach ( var item in Colliders )
      {
        if(MaxTargets != -1 && targets >= MaxTargets) break;
        bool dealtDamage = OnTriggerUpdate( item );
        if(dealtDamage) targets++;
      }
      NextHit = Time.Now + Cooldown;
    }
  }

  public bool OnTriggerUpdate( Collider collider )
  {
      if(!master.Stats.Alive) return false;

      if(collider == null || collider.GameObject == null || collider.GameObject.Parent == null || !collider.GameObject.Parent.Tags.Has(OpponentTag)) return false;

      Y otherMaster = GetMasterFromCollider(collider);
      if ( otherMaster == null || otherMaster == master) return false;

      Swing(otherMaster);
      return true;
  }

  protected virtual void Swing(Y otherMaster){
    if(Knockback > 0f) ApplyKnockback(otherMaster);

    DamageInfo damage = OnHit();
    otherMaster.CallEventReceiveDamage(damage);
    master.CallEventDealDamage(damage);
  }

  protected virtual DamageInfo OnHit(){
    return new DamageInfo(Damage, master.GameObject, GameObject);
  }

  protected virtual void ApplyKnockback(Y otherMaster){
    GameObject otherGameObject = otherMaster.GameObject;
    Rigidbody rigidbody = otherGameObject.Components.Get<Rigidbody>();
    if(rigidbody == null) return;

    Vector3 direction = (otherGameObject.WorldPosition - master.GameObject.WorldPosition).Normal;
    direction.z = 0;

    otherMaster.CallEventKnockback(KnockbackDuration);

    rigidbody.ApplyImpulse(direction * Knockback);
  }

  protected Y GetMasterFromCollider(Collider collider) {
    if(collider.GameObject == null) return null;
    Y otherMaster = collider.GameObject.Components.GetInAncestorsOrSelf<Y>() ?? collider.GameObject.Components.GetInChildrenOrSelf<Y>();
    return otherMaster;
  }
}
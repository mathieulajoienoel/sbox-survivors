public abstract class WeaponBase<T, Y> : Component, Component.ITriggerListener
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

  private float NextHit { get; set; } = 0f;
  private HashSet<Collider> Colliders = new HashSet<Collider>();
  private T master;

  protected override void OnEnabled(){
    master = Components.GetInAncestorsOrSelf<T>();

    ApplyStartingPosition();
    ApplyStartingRotation();
    ApplySpeed();
    ApplyRange();
    ApplySize();
  }
  protected virtual void ApplyStartingPosition(){
    Transform.LocalPosition = Position;
  }
  protected virtual void ApplyStartingRotation(){
    Transform.LocalRotation = Rotation.From(StartRotation);
  }
  protected virtual void ApplyRange(){
    //Transform.LocalScale = Range;
  }
  protected virtual void ApplySpeed(){
  }

  protected virtual void ApplySize(){
    Transform.LocalScale = Size;
  }

  public void OnTriggerEnter( Collider other )
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
      if(collider.GameObject == null || !collider.GameObject.Parent.Tags.Has(OpponentTag)) return false;

      Y otherMaster = GetMasterFromCollider(collider);
      if ( otherMaster == null) return false;

      DamageInfo damage = new DamageInfo(Damage, master.GameObject, GameObject);
      otherMaster.CallEventReceiveDamage(damage);
      master.CallEventDealDamage(damage);
      return true;
  }

  protected Y GetMasterFromCollider(Collider collider) {
    if(collider.GameObject == null) return null;
    Y otherMaster = collider.GameObject.Components.GetInAncestorsOrSelf<Y>() ?? collider.GameObject.Components.GetInChildrenOrSelf<Y>();
    return otherMaster;
  }

}
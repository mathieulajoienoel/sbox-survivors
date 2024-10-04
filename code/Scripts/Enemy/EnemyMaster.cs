public sealed class EnemyMaster : EntityMaster
{
  [RequireComponent] public EnemyTarget Target { get; set; }
  [RequireComponent] public new EnemyStats Stats { get; set; }

  //protected override void OnAwake(){
  //  base.OnAwake();
  //  Target = Components.Get<EnemyTarget>();
  //  Stats = Components.Get<EnemyStats>();
  //}

  public override void Reset(){
    // Reset the rigidbody
    Rigidbody rigidbody = Components.Get<Rigidbody>(true);
    PhysicsLock physicsLock = new PhysicsLock();
    physicsLock.Z = true;
    physicsLock.Pitch = true;
    physicsLock.Roll = true;
    rigidbody.Locking = physicsLock;

    rigidbody.RigidbodyFlags = RigidbodyFlags.DisableCollisionSounds;
    rigidbody.MotionEnabled = true;
  }
}
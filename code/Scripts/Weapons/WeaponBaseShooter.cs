
public sealed class WeaponBaseShooter : Component
{
  private EntityMaster master;
  [Property] public float Damage { get; set; }
  [Property] public float Speed { get; set; }
  [Property] public float Cooldown { get; set; }
  //protected Vector3 ProjectileRange { get; set; }
  //protected Vector3 ProjectileSize { get; set; }
  //protected Vector3 Position { get; set; }
  //protected Angles StartRotation { get; set; }
  [Property] public GameObject[] ProjectileSource { get; set; }
  [Property] public GameObject WeaponProjectile { get; set; }

  private float NextHit { get; set; } = 0f;

  protected override void OnEnabled(){
    master = Components.GetInAncestorsOrSelf<EntityMaster>();
  }

  protected override void OnFixedUpdate()
  {
    if(NextHit == 0f || NextHit < Time.Now){
      foreach(var projectileSource in ProjectileSource){
        Shoot(projectileSource.Transform);
      }
      NextHit = Time.Now + Cooldown;
    }
  }

  private void Shoot(GameTransform sourceTransform){
    Vector3 position = sourceTransform.Position;
    position.z = 12.5f;
    GameObject projectile = WeaponProjectile.Clone(new CloneConfig(new Transform(position), null, false));
    if(projectile == null) return;
    WeaponBaseProjectile projectileComponent = projectile.Components.Get<WeaponBaseProjectile>(true);
    if(projectileComponent == null) return;
    projectileComponent.Damage = OnHit();
    projectileComponent.Speed = Speed;
    projectileComponent.Direction = sourceTransform.Rotation.Forward;
    projectileComponent.master = master;
    projectile.Enabled = true;
  }

  private DamageInfo OnHit(){
    //return new DamageInfo(Damage, null, null);
    return new DamageInfo(Damage, master.GameObject, GameObject);
  }
}
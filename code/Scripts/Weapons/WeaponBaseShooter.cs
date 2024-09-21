public abstract class WeaponBaseShooter : Component, IHolsteredWeapon
{
  private EntityMaster master;
  [Property] public float Damage { get; set; }
  [Property] public float Speed { get; set; }
  [Property] public float Cooldown { get; set; }
  [Property] public float ProjectileDuration { get; set; } = 5f;
  [Property] public GameObject[] ProjectileSource { get; set; }
  [Property] public GameObject WeaponProjectile { get; set; }
  [Property] public HolsterType WeaponHolster { get; set; } = HolsterType.AimedWeaponHolster;
  public abstract string ProjectilePool { get; set; }

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
    GameObject projectile = ObjectPool.Instance.GetObjectFromPool(ProjectilePool);
    //GameObject projectile = WeaponProjectile.Clone(new CloneConfig(new Transform(position), null, false));
    // @@TODO bug here, projectile doesn't reset properly
    if(projectile == null) return;
    projectile.Transform.Position = position;
    WeaponBaseProjectile projectileComponent = projectile.Components.Get<WeaponBaseProjectile>(true);
    if(projectileComponent == null) return;
    projectileComponent.ApplyAttributes(
      master,
      OnHit(),
      Speed,
      sourceTransform.Rotation.Forward,
      ProjectileDuration,
      ProjectilePool
    );
  }

  private DamageInfo OnHit(){
    return new DamageInfo(Damage, master.GameObject, GameObject);
  }
}
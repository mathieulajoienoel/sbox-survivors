public abstract class PlayerWeaponBase : WeaponBase<PlayerMaster, EnemyMaster> {
  protected override string OpponentTag { get; set; } = "Enemy";
}
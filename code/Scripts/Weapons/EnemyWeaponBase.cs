public abstract class EnemyWeaponBase : WeaponBase<EnemyMaster, PlayerMaster> {
  protected override string OpponentTag { get; set; } = "Player";
}
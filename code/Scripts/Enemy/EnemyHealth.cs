public sealed class EnemyHealth : EntityHealth<EnemyMaster>
{
  public override void OnDeath(){
    base.OnDeath();
    GameMaster.Instance.CallEnemyDeathEvent();
  }
}

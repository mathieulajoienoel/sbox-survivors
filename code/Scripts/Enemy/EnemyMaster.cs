public sealed class EnemyMaster : EntityMaster
{
  public EnemyTarget Target;
  public new EnemyStats Stats { get; set; }

  protected override void OnAwake(){
    base.OnAwake();
    Target = Components.Get<EnemyTarget>();
    Stats = Components.Get<EnemyStats>();
  }
}
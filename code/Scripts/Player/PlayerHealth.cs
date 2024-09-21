public sealed class PlayerHealth : EntityHealth<PlayerMaster>
{
  public override void CheckDeath(){
    if(CurrentHealth < 1 && master.Stats.Alive){
      master.CallEventDeath();
      GameMaster.Instance.CallPlayerDeathEvent();
    }
  }

}

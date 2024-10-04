public sealed class EnemyController : EntityController<EnemyMaster>
{
	protected override void OnFixedUpdate()
  {
    // Decide which direction to go
    // Move
    if(canMove) Move(GetPlayerDirection());
  }

  public Vector3 GetPlayerDirection(){
    Vector3 direction = Vector3.Forward;
    PlayerMaster target = master.Target.Target;
    if(target != null){
      direction = (WorldPosition - target.WorldPosition).Normal * -1;
    }

    return direction;
  }
}

public sealed class PlayerController : EntityController<PlayerMaster>
{
	protected override void OnFixedUpdate()
  {
    if(canMove) Move(Input.AnalogMove);
  }
}

public sealed class Teleporter : Component, Component.ITriggerListener {
  [Property] public GameObject OtherEnd { get; set; }
  [Property] public float Cooldown { get; set; } = 5f;
  [RequireComponent] public SpriteRenderer spriteRenderer { get; set; }
  private bool CanTeleport = true;
  private float NextTeleport = 0f;

	public void OnTriggerEnter( Collider other )
  {
    TeleportEntity( other );
  }
  //public void OnTriggerExit( Collider other )
  //{
  //  TeleportEntity( other );
  //}

	protected override void OnUpdate()
	{
		if(!CanTeleport && Time.Now > NextTeleport){
      EnableTeleport();
    }
	}

	private void TeleportEntity(Collider other) {
    if(!CanTeleport) return;

    GameObject obj = null;
    if(other.GameObject.IsRoot) obj = other.GameObject;
    if(other.GameObject.Parent.IsRoot) obj = other.GameObject.Parent;
    if(obj == null) return;
    Log.Info(obj);

    OtherEnd.Components.Get<Teleporter>().Teleport(obj);

    DisableTeleport();
  }

  public void Teleport(GameObject obj){
    GameMaster.Instance.CallTeleportEvent(obj);
    obj.Transform.Position = Transform.Position + new Vector3(GameMaster.Instance.Rand(-20,20), GameMaster.Instance.Rand(-20,20), Transform.Position.z);

    DisableTeleport();
  }

  private void DisableTeleport(){
    NextTeleport = Time.Now + Cooldown;
    CanTeleport = false;
    spriteRenderer.Color = Color.Red;
  }
  private void EnableTeleport(){
    CanTeleport = true;
    NextTeleport = 0f;
    spriteRenderer.Color = Color.White;
  }
}
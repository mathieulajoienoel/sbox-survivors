public abstract class EntityController<T> : Component where T : EntityMaster
{
  protected T master;

  protected Vector3 Speed;
  protected Angles forcedRotation = Rotation.From(new Angles(0, -90, 0));
  [Property] protected bool canMove { get; set; } = true;


  protected override void OnEnabled(){
    master = Components.Get<T>();
    //WorldPosition = new Vector3(WorldPosition.x, WorldPosition.y, 12.5f);

    master.EventKnockback += OnKnockback;
    master.EventKnockbackRelease += OnKnockbackRelease;
    master.EventStart += Start;
  }

  private void Start(){
    float step = master.Stats.MoveSpeed;
    // Set the move speed to use
    Speed = new Vector3(step, step, 0);
    canMove = true;
  }

  protected override void OnDisabled(){
    master.EventKnockback -= OnKnockback;
    master.EventKnockbackRelease -= OnKnockbackRelease;
    master.EventStart -= Start;
  }

  private void OnKnockback (float KnockbackDuration){
    if(!master.Stats.Alive && !master.Health.CanBeDamaged) return;
    canMove = false;
  }
  private void OnKnockbackRelease (){
    canMove = true;
  }

  public void ToggleMove(){
    canMove = !canMove;
  }

  protected void RotateCharacter(float rotY){
    master.Character.RotateTo(rotY);
  }

  protected void Move(Vector3 direction){
    // Rotate character
    float rotY = -90f;
    if(direction.x > 0 && direction.y > 0) rotY = 45f;
    else if(direction.y > 0 && direction.x < 0) rotY = 135f;
    else if(direction.x < 0 && direction.y < 0) rotY = 225f;
    else if(direction.y < 0 && direction.x > 0) rotY = 315f;
    else if(direction.x > 0) rotY = 0f;
    else if(direction.y > 0) rotY = 90f;
    else if(direction.x < 0) rotY = 180f;
    else if(direction.y < 0) rotY = 270f;
    if(direction.x != 0 || direction.y != 0){
      RotateCharacter(rotY);
    }

    // Move character
    Vector3 moveTo = WorldPosition.LerpTo(WorldPosition + (direction * Speed), Time.Delta);
    moveTo.z = 12.5f;
    WorldPosition = moveTo;
    // Force game object rotation
    LocalRotation = forcedRotation;
  }

}

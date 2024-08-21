public abstract class EntityController<T> : Component where T : EntityMaster
{
  protected T master;

  protected Vector3 Speed;
  protected Angles forcedRotation = Rotation.From(new Angles(0, -90, 0));
  protected bool canMove = true;

  protected override void OnEnabled(){
    master = Components.Get<T>();

    float step = master.Stats.MoveSpeed;
    // Set the move speed to use
    Speed = new Vector3(step, step, 0);
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
    Vector3 moveTo = Transform.Position.LerpTo(Transform.Position + (direction * Speed), Time.Delta);
    moveTo.z = 12.5f;
    Transform.Position = moveTo;
    // Force game object rotation
    Transform.LocalRotation = forcedRotation;
  }

}

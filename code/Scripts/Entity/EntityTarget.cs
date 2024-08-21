public abstract class EntityTarget<T> : Component
{
  public T Target;
  protected override void OnEnabled(){
    FindTarget();
  }
  private void FindTarget(){
    foreach ( var target in Scene.GetAllComponents<T>() )
    {
      Target = target;
      break;
    }
  }

}

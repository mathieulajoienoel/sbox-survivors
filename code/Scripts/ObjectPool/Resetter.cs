public sealed class Resetter : Component {
  public GameObject ResetAll(){
    GameObject.Enabled = false;
    foreach (Component component in Components.GetAll(FindMode.EverythingInSelfAndChildren))
    {
      if(component == this) continue;
      component.Reset();
    }
    return GameObject;
  }
}
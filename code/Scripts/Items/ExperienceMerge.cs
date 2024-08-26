using System.Threading.Tasks;

public sealed class ExperienceMerge : Component, Component.ITriggerListener {
  /*public bool IsMerging = false;
  public async void OnTriggerEnter( Collider other )
  {
    _ = Merge(other);
  }

  async public Task Merge(Collider other){
    Game.IsPaused = true;
    if(other == null) {
      IsMerging = false;
      return;
    }
    if(other.GameObject == GameObject.Parent || other.GameObject == GameObject) {
      IsMerging = false;
      return;
    }
    GameObject otherParent = other.GameObject.Parent;
    if(otherParent == null) {
      IsMerging = false;
      return;
    }
    if(!other.GameObject.Tags.Has("experience")) {
      IsMerging = false;
      return;
    }
    Experience otherExperience = other.GameObject.Parent.Components.GetInChildrenOrSelf<Experience>();
    if(otherExperience == null) {
      IsMerging = false;
      return;
    }
    Log.Info(other);
    IsMerging = true;
    ExperienceMerge otherExperienceMerge = other.GameObject.Parent.Components.GetInChildrenOrSelf<ExperienceMerge>();

    if(otherExperienceMerge.IsMerging == true) {
      IsMerging = false;
      return;
    }
    Item otherItem = otherExperience.Collect();
    //if(otherItem == null || otherItem.Type != CollectableType.Experience) {
      //IsMerging = false;
    //  return;
    //}
    Item myItem = GameObject.Parent.Components.GetInChildrenOrSelf<Item>();
    myItem.Value += otherItem.Value;
    //other.GameObject.Parent.Destroy();
    await Task.Frame();
  }*/
}
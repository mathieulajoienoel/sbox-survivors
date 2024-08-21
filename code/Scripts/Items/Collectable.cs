public abstract class Collectable : Component {
  public Item Collect(){
    Item item = Components.Get<Item>();
    GameObject.Parent.Destroy();
    return item;
  }
}
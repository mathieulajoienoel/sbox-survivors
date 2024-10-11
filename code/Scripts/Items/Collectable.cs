public abstract class Collectable : Component {
  [Property] public abstract string ObjPool { get; set; }

  public Item Collect(){
    Item item = Components.Get<Item>();
		//GameObject.Parent.Destroy();
    // Clone object, since it will be reinitialised in ReturnToPool
		Item toReturn = new Item
		{
			Type = item.Type,
			Value = item.Value,
			item = item.item
		};

		ReturnToPool();
    return toReturn;
  }

  public virtual void ReturnToPool(){
    GameObject.Parent.Destroy();
    //ObjectPool.Instance.ReturnToPool(ObjPool, GameObject.Parent);
  }
}
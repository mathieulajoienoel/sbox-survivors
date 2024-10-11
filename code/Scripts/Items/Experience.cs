public sealed class Experience : Collectable {
  [Property] public override string ObjPool { get; set; } = "Experience";
  public override void ReturnToPool(){
    ObjectPool.Instance.ReturnToPool(ObjPool, GameObject.Parent);
  }
}
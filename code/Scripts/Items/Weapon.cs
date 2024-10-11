public sealed class Weapon : Collectable {
  [Property] public override string ObjPool { get; set; } = null;
  //public override void ReturnToPool(){
  //  GameObject.Parent.Destroy();
  //  //ObjectPool.Instance.ReturnToPool(ObjPool, GameObject.Parent);
  //}
}
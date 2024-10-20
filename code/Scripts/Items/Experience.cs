public sealed class Experience : Collectable {
  [Property] public override string ObjPool { get; set; } = "Experience";
  public override void ReturnToPool(){
    ObjectPool.Instance.ReturnToPool(ObjPool, GameObject.Parent);
  }

	protected override void OnEnabled()
	{
		base.OnEnabled();
    SetColorByItemValue();
	}

  public override void Reset()
	{
		base.Reset();
    ModelRenderer modelRenderer = Components.Get<ModelRenderer>(true);
    modelRenderer.Model = Model.Sphere;
	}

  private void SetColorByItemValue(){
    Item item = Components.Get<Item>();
    ModelRenderer modelRenderer = Components.Get<ModelRenderer>(true);
    if(item == null || modelRenderer == null){
      return;
    }
    if(item.Value >= 10) {
      modelRenderer.Tint = Color.Red;
    } else if (item.Value >= 50) {
      modelRenderer.Tint = Color.Orange;
    } else if (item.Value >= 100) {
      modelRenderer.Tint = Color.Yellow;
    } else if (item.Value >= 1000) {
      modelRenderer.Tint = Color.Magenta;
    } else {
      modelRenderer.Tint = Color.Cyan;
    }
  }
}
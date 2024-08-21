public sealed class DrawColliderBox : DebugTool {
  private ModelRenderer renderer;
  protected override void OnDebug(){
    renderer = Components.Create<ModelRenderer>();
    renderer.Model = Model.Cube;
    renderer.Tint = Color.Transparent;
  }
  protected override void OnFixedUpdate()
	{
    Gizmo.Draw.LineBBox(renderer.Bounds);
	}
}
public sealed class ColorActor : DebugTool {
  protected override void OnDebug(){
    ModelRenderer modelRenderer = Components.Get<ModelRenderer>();
    if(GameObject.Tags.Has("enemy_character")){
      modelRenderer.Tint = Color.Yellow;
    } else {
      modelRenderer.Tint = Color.Blue;
    }
  }
}
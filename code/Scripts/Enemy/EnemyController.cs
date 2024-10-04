public sealed class EnemyController : EntityController<EnemyMaster>
{
	protected override void OnFixedUpdate()
  {
    // Decide which direction to go
    // Move
    if(canMove) Move(GetPlayerDirection());
  }

  public Vector3 GetPlayerDirection(){
    Vector3 direction = Vector3.Forward;
    PlayerMaster target = master.Target.Target;
    if(target != null){
      direction = (WorldPosition - target.WorldPosition).Normal * -1;
    }

    return direction;
  }

  public override void Reset(){
    // Reset the model renderer
    ModelRenderer modelRenderer = null;
    foreach(ModelRenderer item in Components.GetAll<ModelRenderer>(FindMode.EverythingInSelfAndDescendants)){
      if(item.GameObject.Name == "EnemyCharacter"){
        modelRenderer = item;
        break;
      }
    }
    modelRenderer.Tint = Color.White;
    modelRenderer.Model = Model.Cube;
  }
}

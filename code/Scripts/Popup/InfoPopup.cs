public abstract class InfoPopup : Component
{
  protected PopupType Type { get; set; }
  [RequireComponent] protected TextRenderer TextDisplay { get; set; }
  [Property] public abstract string PopupPool { get; set; }
  [Property] public virtual float TimeToWait { get; set; } = 1f;
  [Property] public virtual float DestroyAfter { get; set; } = 0.25f;
  [Property] public virtual float Speed { get; set; } = 10f;

	// OnUpdate is called every frame
  protected override void OnEnabled(){
    Vector3 position = Transform.Position;
    position.z = 25;
    Transform.Position = position;
    Transform.Rotation = Rotation.From(new Angles(90,0,0));
  }

	protected void ResetDisplay(){
    TextDisplay.Scale = 0.25f;
    TextDisplay.FontSize = 32f;
    Transform.Scale = new Vector3(1,1,1);
  }

  public abstract void Display(float value);
}
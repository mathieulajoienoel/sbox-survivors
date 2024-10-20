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
    Vector3 position = WorldPosition;
    position.z = 25;
    WorldPosition = position;
    WorldRotation = Rotation.From(new Angles(90,0,0));
  }

	protected void ResetDisplay(){
    TextDisplay.Scale = 0.25f;
    TextDisplay.FontSize = 32f;
    WorldScale = new Vector3(1,1,1);

    TextRendering.Scope TextScope = new TextRendering.Scope();
    TextScope.FontWeight = 400;
    TextScope.LineHeight = 1f;
    TextScope.FontSize = 32f;
    TextScope.FontName = "Roboto";
    TextScope.TextColor = Color.White;
    TextDisplay.TextScope = TextScope;
  }

  public abstract void Display(float value);
}
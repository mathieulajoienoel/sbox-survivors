public abstract class InfoPopup : Component
{
  protected PopupType Type { get; set; }
  protected TextRenderer TextDisplay { get; set; }
	// OnUpdate is called every frame
  protected override void OnEnabled(){
    TextDisplay = Components.Get<TextRenderer>();
    Vector3 position = Transform.Position;
    position.z = 25;
    Transform.Position = position;
    Transform.Rotation = Rotation.From(new Angles(90,0,0));
  }

	public abstract void Display(float value);
}
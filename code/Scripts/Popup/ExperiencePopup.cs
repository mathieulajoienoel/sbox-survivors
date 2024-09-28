public sealed class ExperiencePopup : InfoPopup {
  [Property] public override string PopupPool { get; set; } = "ExperiencePopup";
  [Property] public override float TimeToWait { get; set; } = 1f;
  [Property] public override float DestroyAfter { get; set; } = 0.25f;
  [Property] public override float Speed { get; set; } = 10f;
  public override void Display(float value){
    ResetDisplay();
    string toDisplay = "";
    TextDisplay.Color = Color.Cyan;
    toDisplay += "🔷";
    TextDisplay.Text = toDisplay + value.ToString();

    PopupFadeOutAfterTime fader = Components.Get<PopupFadeOutAfterTime>(true);
    fader?.SetTimings(1f, 0.25f, 10f);

    GameObject.Enabled = true;
  }
}
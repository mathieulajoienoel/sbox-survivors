public sealed class DamagePopup : InfoPopup {
  [Property] public override string PopupPool { get; set; } = "DamagePopup";
  [Property] public override float TimeToWait { get; set; } = 0.25f;
  [Property] public override float DestroyAfter { get; set; } = 0.25f;
  [Property] public override float Speed { get; set; } = 10f;
  public override void Display(float value){
    ResetDisplay();
    Color color;
    string toDisplay = "";
    if(value == 0){
      color = Color.White;
      toDisplay += "🛡️";
    } else if(value > 0){
      color = Color.Green;
      toDisplay += "➕";
    } else {
      color = Color.Red;
      toDisplay += "🗡️";
    }

    TextDisplay.Text = toDisplay + value.ToString();
    TextDisplay.Color = color;

    PopupFadeOutAfterTime fader = Components.Get<PopupFadeOutAfterTime>(true);
    fader?.SetTimings(TimeToWait, DestroyAfter, Speed);

    GameObject.Enabled = true;
  }
}
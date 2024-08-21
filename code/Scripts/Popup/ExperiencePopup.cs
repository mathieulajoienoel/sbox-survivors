public sealed class ExperiencePopup : InfoPopup {
  public override void Display(float value){
    string toDisplay = "";
    TextDisplay.Color = Color.Cyan;
    toDisplay += "🔷";
    TextDisplay.Text = toDisplay + value.ToString();
    ;
  }
}
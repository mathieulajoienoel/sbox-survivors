public sealed class DamagePopup : InfoPopup {
  public override void Display(float value){
    string toDisplay = "";
    if(value == 0){
      TextDisplay.Color = Color.White;
      toDisplay += "🛡️";
    } else if(value > 0){
      TextDisplay.Color = Color.Green;
      toDisplay += "➕";
    } else {
      TextDisplay.Color = Color.Red;
      toDisplay += "🗡️";
    }
    TextDisplay.Text = toDisplay + value.ToString();
    ;
  }
}
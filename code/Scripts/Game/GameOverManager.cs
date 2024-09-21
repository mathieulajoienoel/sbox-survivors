public sealed class GameOverManager : Component {
  [RequireComponent] public GameMaster master { get; set; }
  [Property] public GameObject GameOverPanel { get; set; }

  protected override void OnEnabled() {
    master.PlayerDeathEvent += OnPlayerDeath;
    master.GameWinEvent += OnGameWin;
  }
  protected override void OnDisabled() {
    master.PlayerDeathEvent -= OnPlayerDeath;
    master.GameWinEvent -= OnGameWin;
  }

  private void OnPlayerDeath(){
    Log.Info("Game over");
    ShowGameOverPanel(true);
  }

  private void OnGameWin(){
    Log.Info("You win!");
    ShowGameOverPanel(false);
  }

  private void ShowGameOverPanel(bool HasWon = false){
    GameObject panel = GameOverPanel.Clone(Vector3.Zero);
    if(HasWon){
      panel.Components.Get<GameOverPanel>().OnGameWin();
    } else {
      panel.Components.Get<GameOverPanel>().OnPlayerDeath();
    }
    Game.IsPaused = true;
  }
}
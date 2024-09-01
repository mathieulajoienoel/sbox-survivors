public sealed class GameWinManager : Component {
  [RequireComponent] public GameMaster master { get; set; }

  protected override void OnEnabled() {
    master.GameWinEvent += OnGameWin;
  }
  protected override void OnDisabled() {
    master.GameWinEvent -= OnGameWin;
  }

  private void OnGameWin(){
    Game.IsPaused = true;
    // @@TODO show game win screen

    Log.Info("You win!");
  }
}
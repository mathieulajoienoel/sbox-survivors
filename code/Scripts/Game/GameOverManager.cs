public sealed class GameOverManager : Component {
  [RequireComponent] public GameMaster master { get; set; }

  protected override void OnEnabled() {
    master.PlayerDeathEvent += OnPlayerDeath;
  }
  protected override void OnDisabled() {
    master.PlayerDeathEvent -= OnPlayerDeath;
  }

  private void OnPlayerDeath(){
    Game.IsPaused = true;
    // @@TODO show gameover screen
    Log.Info("Game over");
  }
}
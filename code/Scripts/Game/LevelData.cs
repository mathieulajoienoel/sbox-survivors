public sealed class LevelData : Component {
  [Property] public int EnemiesPerWave { get; set; } = 5;
  [Property] public int TotalWaves { get; set; } = 10;
  [Property] public double DifficultyMultiplier { get; set; } = 1.5;
  [Property] public GameObject[] EnemyPrefabs { get; set; }
}
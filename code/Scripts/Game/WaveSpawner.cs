public sealed class WaveSpawner : Component {
  [RequireComponent] public LevelData LevelData { get; set; }
  [RequireComponent] public GameMaster GameMaster { get; set; }
  [Property] public int MaximumEnemiesOnScreen { get; set; } = 30;



  [Property] public GameObject[] EnemySpawnPoints { get; set; }

  [Property] public int CurrentEnemyCount { get; set; } = 0;
  [Property] public int CurrentWave { get; set; } = 1;
  [Property] public int TotalEnemiesThisWave { get; set; } = 0;

  [Property] public string EnemyPool { get; set; } = "EnemyPool";


	protected override void OnFixedUpdate()
	{
		//if(CurrentEnemyCount > 1) return; //
    if(CurrentWave > LevelData.TotalWaves && CurrentEnemyCount < 1){ // check win state. end wave, with no more enemies
      //@@TODO win state
      GameMaster.Instance.CallGameWinEvent();
      return;
    }
    if(TotalEnemiesThisWave < LevelData.EnemiesPerWave) // if we need more enemies to spawn
    {
      if(CurrentEnemyCount < MaximumEnemiesOnScreen) SpawnEnemies(); // if we can spawn more enemies
    } else { // Else start next wave
      CurrentWave = NextWave(CurrentWave);
    }
	}

  private void SpawnEnemies(){
    if(EnemySpawnPoints.Length < 1) return;
    int total = LevelData.EnemiesPerWave - TotalEnemiesThisWave;
    if(total > MaximumEnemiesOnScreen) total = MaximumEnemiesOnScreen;
    total -= CurrentEnemyCount;
    if(total < 1) return;
    Log.Info("Spawning " + total + " Enemies");
    for (int i = 0; i < total; i++)
    {
      Vector3 position = EnemySpawnPoints[GameMaster.Instance.Rand(0,EnemySpawnPoints.Length - 1)].WorldPosition;
      position += new Vector3(GameMaster.Instance.Rand(-100,100), GameMaster.Instance.Rand(-100,100), 12.5f);
      // Spawn a random enemy from the possible enemies for this level
      /*GameObject enemy = LevelData.EnemyPrefabs[GameMaster.Instance.Rand(0, LevelData.EnemyPrefabs.Length - 1)].Clone(new CloneConfig(new Transform(position), null, false));
      enemy.Enabled = true;*/
      //LevelData.EnemyPrefabs[GameMaster.Instance.Rand(0, LevelData.EnemyPrefabs.Length - 1)].Clone(position);

      GameObject enemy = ObjectPool.Instance.GetObjectFromPool(EnemyPool);
      enemy.WorldPosition = position;
      enemy.Parent = null;
      enemy.Enabled = true;
      // @@TODO apply enemy type and loadout

      CurrentEnemyCount++;
      TotalEnemiesThisWave++;
    }
  }

	private int NextWave(int waveNumber){
    Log.Info("Spawning wave " + waveNumber);
    TotalEnemiesThisWave = 0;

    LevelData.DifficultyMultiplier += 0.5f;
    LevelData.EnemiesPerWave = (int)(LevelData.EnemiesPerWave * LevelData.DifficultyMultiplier);

    return waveNumber + 1;
  }
  protected override void OnEnabled(){
    // Get the player
    GameObject player = GameMaster.Instance.Player;

    // Get the spawn points
    EnemySpawnPoints = player.Children.Find(x => x.Name == "EnemySpawnPoints").Children.ToArray();
		GameMaster.Instance.EnemyDeathEvent += UpdateEnemyCount;
	}
	protected override void OnDisabled(){
		GameMaster.Instance.EnemyDeathEvent -= UpdateEnemyCount;
	}

	private void UpdateEnemyCount(){
		CurrentEnemyCount--;
	}
}
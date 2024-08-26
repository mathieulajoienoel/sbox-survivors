using System;
public sealed class GameMaster : Component {

  public static GameMaster Instance;

  [Property] public bool DebugMode { get; set; } = false;
  [Property] private GameObject PlayerPrefab { get; set; }
  [Property] public GameObject EnemyPrefab { get; set; }
  [Property] private GameObject PlayerSpawnPoint { get; set; }
  [Property] private GameObject[] EnemySpawnPoints { get; set; }
  [Property] public GameObject DamagePopupPrefab { get; set; }
  [Property] public GameObject ExperiencePopupPrefab { get; set; }
  [Property] public GameObject ExperiencePrefab { get; set; }

  [Property] public GameObject Player { get; set; }

  public Random RandomGenerator = new Random();

  //private Vector3 ObjectPoolSpawnPoint { get; set; } = new Vector3(-100,-100,-100);

  public delegate void EnemyDeathEventHandler();
  public event EnemyDeathEventHandler EnemyDeathEvent;
  public delegate void ExperienceGainEventHandler(float value);
  public event ExperienceGainEventHandler ExperienceGainEvent;
  public void CallEnemyDeathEvent(){
		EnemyDeathEvent?.Invoke();
	}
  public void CallExperienceGainEvent(float value){
		ExperienceGainEvent?.Invoke(value);
	}

	protected override void OnAwake()
	{
		base.OnAwake();
    Player = PlayerPrefab.Clone(PlayerSpawnPoint.Transform.Position);

    /*GameObject player = PlayerPrefab.Clone(PlayerSpawnPoint.Transform.Position);

    // If no EnemySpawnPoints are defined, use the ones on the player
    if(EnemySpawnPoints.Length < 1){
      EnemySpawnPoints = player.Children.Find(x => x.Name == "EnemySpawnPoints")?.Children?.ToArray();
    }

    foreach (var spawnPoint in EnemySpawnPoints)
    {
      Log.Info(spawnPoint.Transform.Position);
      EnemyPrefab.Clone(spawnPoint.Transform.Position);
    }*/

    Instance = this;
	}
  public int Rand(int min, int max){
    return RandomGenerator.Next(min, max);
  }
}
using System;
public sealed class GameMaster : Component {

  public static GameMaster Instance;

  [Property] public bool DebugMode { get; set; } = false;
  [Property] private GameObject PlayerPrefab { get; set; }
  [Property] private GameObject EnemyPrefab { get; set; }
  [Property] private GameObject PlayerSpawnPoint { get; set; }
  [Property] private GameObject[] EnemySpawnPoints { get; set; }
  [Property] public GameObject DamagePopupPrefab { get; set; }
  [Property] public GameObject ExperiencePopupPrefab { get; set; }
  [Property] public GameObject ExperiencePrefab { get; set; }

  public Random RandomGenerator = new Random();

  //private Vector3 ObjectPoolSpawnPoint { get; set; } = new Vector3(-100,-100,-100);

	protected override void OnAwake()
	{
		base.OnAwake();
    PlayerPrefab.Clone(PlayerSpawnPoint.Transform.Position);

    foreach (var spawnPoint in EnemySpawnPoints)
    {
      EnemyPrefab.Clone(spawnPoint.Transform.Position);
    }

    Instance = this;
	}
  public int Rand(int min, int max){
    return RandomGenerator.Next(min, max);
  }
}
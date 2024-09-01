using System;
public sealed class GameMaster : Component {

  private static GameMaster instance = null;
  static GameMaster() {
  }
  public static GameMaster Instance {
    get { return instance; }
  }

  [RequireComponent] public LevelData LevelData { get; set; }

  [Property] public bool DebugMode { get; set; } = false;
  [Property] private GameObject PlayerPrefab { get; set; }
  [Property] private GameObject PlayerSpawnPoint { get; set; }
  [Property] public GameObject DamagePopupPrefab { get; set; }
  [Property] public GameObject ExperiencePopupPrefab { get; set; }
  [Property] public GameObject ExperiencePrefab { get; set; }

  [Property] public GameObject Player { get; set; }

  public Random RandomGenerator = new Random();

  //private Vector3 ObjectPoolSpawnPoint { get; set; } = new Vector3(-100,-100,-100);

  public delegate void EnemyDeathEventHandler();
  public event EnemyDeathEventHandler EnemyDeathEvent;
  public delegate void PlayerDeathEventHandler();
  public event PlayerDeathEventHandler PlayerDeathEvent;
  public delegate void GameWinEventHandler();
  public event GameWinEventHandler GameWinEvent;
  public delegate void ExperienceGainEventHandler(float value);
  public event ExperienceGainEventHandler ExperienceGainEvent;
  public delegate void TeleportEventHandler(GameObject TeleportedObject);
  public event TeleportEventHandler TeleportEvent;
  public void CallEnemyDeathEvent(){
		EnemyDeathEvent?.Invoke();
	}
  public void CallTeleportEvent(GameObject TeleportedObject){
		TeleportEvent?.Invoke(TeleportedObject);
	}
  public void CallPlayerDeathEvent(){
		PlayerDeathEvent?.Invoke();
	}
  public void CallGameWinEvent(){
		GameWinEvent?.Invoke();
	}
  public void CallExperienceGainEvent(float value){
		ExperienceGainEvent?.Invoke(value);
	}

	protected override void OnAwake()
	{
		base.OnAwake();
    instance = this;
    Player = PlayerPrefab.Clone(PlayerSpawnPoint.Transform.Position);
	}
  public int Rand(int min, int max){
    return RandomGenerator.Next(min, max);
  }
}
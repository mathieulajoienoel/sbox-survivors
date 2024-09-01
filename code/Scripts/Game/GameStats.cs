public sealed class GameStats : Component {
  [RequireComponent] public GameMaster master { get; set; }

  private PlayerMaster playerMaster;

	protected override void OnEnabled()
	{
    ResetStats();
		playerMaster = master.Player.Components.Get<PlayerMaster>();

    master.EnemyDeathEvent += OnEnemyDeath;
    master.PlayerDeathEvent += OnPlayerDeath;
    master.TeleportEvent += OnTeleport;

    playerMaster.EventDealDamage += OnDamageDealt;
    playerMaster.EventReceiveDamage += OnDamageReceived;
    playerMaster.EventCollectItem += OnItemCollect;
    playerMaster.EventGainExperience += OnExperienceGain;
    playerMaster.EventLevelUp += OnLevelUp;
	}

  protected override void OnDisabled()
	{
		master.EnemyDeathEvent -= OnEnemyDeath;
    master.PlayerDeathEvent -= OnPlayerDeath;
    master.TeleportEvent -= OnTeleport;

    playerMaster.EventDealDamage -= OnDamageDealt;
    playerMaster.EventReceiveDamage -= OnDamageReceived;
    playerMaster.EventCollectItem -= OnItemCollect;
    playerMaster.EventGainExperience -= OnExperienceGain;
    playerMaster.EventLevelUp -= OnLevelUp;
	}

  public void ResetStats(){
    Sandbox.Services.Stats.SetValue( "kills", 0 );
    Sandbox.Services.Stats.SetValue( "deaths", 0 );
    Sandbox.Services.Stats.SetValue( "damage_dealt", 0 );
    Sandbox.Services.Stats.SetValue( "damage_received", 0 );
    Sandbox.Services.Stats.SetValue( "experience", 0 );
    Sandbox.Services.Stats.SetValue( "items", 0 );
    Sandbox.Services.Stats.SetValue( "teleported", 0 );
    Sandbox.Services.Stats.SetValue( "level_up", 0 );
  }

  private void OnEnemyDeath(){
    Sandbox.Services.Stats.Increment( "kills", 1 );
  }
  private void OnPlayerDeath(){
    Sandbox.Services.Stats.Increment( "deaths", 1 );
  }
  private void OnDamageDealt(DamageInfo damage){
    Sandbox.Services.Stats.Increment( "damage_dealt", damage.Damage );
  }
  private void OnDamageReceived(DamageInfo damage){
    Sandbox.Services.Stats.Increment( "damage_received", damage.Damage );
  }
  private void OnItemCollect(Item item){
    if(item.Type == CollectableType.Experience) return;
    Sandbox.Services.Stats.Increment( "items", item.Value );
  }
  private void OnExperienceGain(float value){
    Sandbox.Services.Stats.Increment( "experience", value );
  }
  private void OnTeleport(GameObject TeleportedObject){
    if(TeleportedObject.Tags.Has("Player")){
      Sandbox.Services.Stats.Increment( "teleported", 1 );
    }
  }
  private void OnLevelUp(int level){
    Sandbox.Services.Stats.Increment( "level_up", 1 );
  }

}
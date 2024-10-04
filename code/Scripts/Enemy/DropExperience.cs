public sealed class DropExperience : Component {
  private EnemyMaster master;
  private float ExperienceDrop;
  protected override void OnEnabled(){
    master = Components.Get<EnemyMaster>();

    master.EventDeath += OnDeath;
    master.EventStart += Start;
  }
  protected override void OnDisabled()
	{
    master.EventDeath -= OnDeath;
    master.EventStart -= Start;
	}

  private void Start(){
    ExperienceDrop = master.Stats.ExperienceDrop;
  }

  public void OnDeath(){
    // Spawn experience
    GameObject experienceObj = GameMaster.Instance.ExperiencePrefab.Clone(GameObject.WorldPosition);
    Item item = experienceObj.Components.GetInChildrenOrSelf<Item>();
    item.Type = CollectableType.Experience;
    item.Value = ExperienceDrop;
  }
}
public sealed class DropExperience : Component {
  private EnemyMaster master;
  private float ExperienceDrop;
  protected override void OnEnabled(){
    master = Components.Get<EnemyMaster>();
    ExperienceDrop = master.Stats.ExperienceDrop;

    master.EventDeath += OnDeath;
  }
  protected override void OnDisabled()
	{
    master.EventDeath -= OnDeath;
	}

  public void OnDeath(){
    // Spawn experience
    GameObject experienceObj = GameMaster.Instance.ExperiencePrefab.Clone(GameObject.Transform.Position);
    Item item = experienceObj.Components.GetInChildrenOrSelf<Item>();
    item.Type = CollectableType.Experience;
    item.Value = ExperienceDrop;
  }
}
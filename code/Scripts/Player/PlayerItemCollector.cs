public sealed class PlayerItemCollector : Component, Component.ITriggerListener {
  private PlayerMaster master;
  public string ExperiencePopupPool { get; set; } = "ExperiencePopup";

  protected override void OnEnabled(){
    master = Components.GetInParentOrSelf<PlayerMaster>();

    SetCollectRadius();

    master.EventCollectItem += OnCollectItem;
    master.EventGainExperience += OnExperienceGain;
    master.EventGainExperience += ShowExperiencePopup;
  }

  private void SetCollectRadius(){
    float radius = master.GameObject.Components.Get<PlayerStats>().CollectRadius;
    SphereCollider collider = Components.Get<SphereCollider>();
    collider.Radius = radius;
  }

  protected override void OnDisabled()
	{
		base.OnDisabled();
    master.EventCollectItem -= OnCollectItem;
    master.EventGainExperience -= OnExperienceGain;
    master.EventGainExperience -= ShowExperiencePopup;
	}

  private void OnCollectItem(Item item){
    //if(item.Value == 0) return;
    if(item.Type == CollectableType.Experience){
      master.CallEventGainExperience(item.Value);
      return;
    }
    if(item.Type == CollectableType.Weapon){
      GameObject weapon = master.Equipment.GiveWeaponFromPrefab(item.item);
      master.CallEventCollectWeapon(weapon);
      return;
    }
  }

  private void OnExperienceGain(float value){
    master.Stats.Experience += value;
    GameMaster.Instance.CallExperienceGainEvent(value);
  }

  private void ShowExperiencePopup(float value){
    Vector3 position = master.GameObject.WorldPosition;
    position.z = 15;
    position += new Vector3(GameMaster.Instance.Rand(-20,20),GameMaster.Instance.Rand(-20,20), 0);

    GameObject experiencePopup = ObjectPool.Instance.GetObjectFromPool(ExperiencePopupPool);
    if(experiencePopup == null) return;
    experiencePopup.WorldPosition = position;
    //GameObject experiencePopup = GameMaster.Instance.ExperiencePopupPrefab.Clone(position);
    experiencePopup.Components.Get<ExperiencePopup>(true).Display(value);
  }

  public void OnTriggerEnter( Collider other )
	{
		if(!other.GameObject.Tags.Has("collectable")) return;

    Collectable collectable = other.GameObject.Components.Get<Collectable>();
    if(collectable == null) return;
    Item item = collectable.Collect();
    if(item == null) return;
    master.CallEventCollectItem(item);
	}
}
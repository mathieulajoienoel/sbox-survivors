public sealed class PlayerItemCollector : Component, Component.ITriggerListener {
  private PlayerMaster master;
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
      GameObject weapon = GiveWeapon(item);
      master.CallEventCollectWeapon(weapon);
      return;
    }
  }

  private GameObject GiveWeapon(Item item){
    GameObject player = GameMaster.Instance.Player;
    GameObject weapon = item.item.Clone(new CloneConfig(new Transform(player.Transform.Position), player, false));
    IHolsteredWeapon weaponMaster = weapon.Components.GetInChildrenOrSelf<IHolsteredWeapon>(true);
    // Set Weapon in proper holster
    switch(weaponMaster.WeaponHolster){
      case HolsterType.FixedWeaponHolster:
      weapon.SetParent(master.FixedWeaponHolster);
      break;
      case HolsterType.AimedWeaponHolster:
      weapon.SetParent(master.AimedWeaponHolster);
      break;
    }
		weapon.Enabled = true;
    return weapon;
  }

  private void OnExperienceGain(float value){
    master.Stats.Experience += value;
    GameMaster.Instance.CallExperienceGainEvent(value);
  }

  private void ShowExperiencePopup(float value){
    Vector3 position = master.GameObject.Transform.Position;
    position.z = 15;
    position += new Vector3(GameMaster.Instance.Rand(-20,20),GameMaster.Instance.Rand(-20,20), 0);
    GameObject experiencePopup = GameMaster.Instance.ExperiencePopupPrefab.Clone(position);
    experiencePopup.Components.Get<ExperiencePopup>().Display(value);
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
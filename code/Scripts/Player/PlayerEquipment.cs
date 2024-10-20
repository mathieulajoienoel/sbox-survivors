
public sealed class PlayerEquipment : Component {
  [RequireComponent] private PlayerMaster master { get; set; }
  [Property] public List<GameObject> Weapons { get; set; }
  [Property] public List<GameObject> Boons { get; set; }

	/*protected override void OnEnabled()
	{
		master.EventCollectWeapon += OnWeaponCollect;
	}

  protected override void OnDisabled()
	{
		master.EventCollectWeapon -= OnWeaponCollect;
	}*/

  //public void OnWeaponCollect(GameObject weapon){
  //  // Force add collected weapon
  //  AddWeapon(weapon, true);
  //}

	public bool AddWeapon(GameObject weapon, bool force = false){
    if(HasMaxWeapons() && !force){
      return false;
    }
    Weapons.Add(weapon);
    // @@TODO update list display
    return true;
  }
  public bool AddBoon(GameObject boon, bool force = false){
    if(HasMaxBoons() && !force){
      return false;
    }
    Boons.Add(boon);
    // @@TODO update list display
    return true;
  }
  public bool HasMaxWeapons(){
    return Weapons.Count >= master.Stats.MaxWeapons;
  }
  public bool HasMaxBoons(){
    return Boons.Count >= master.Stats.MaxBoons;
  }

  /*public GameObject GiveWeaponFromItem(Item item){
    GameObject player = GameMaster.Instance.Player;
    GameObject weapon = item.item.Clone(new CloneConfig(new Transform(player.WorldPosition), player, false));
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
    AddWeapon(weapon);
    return weapon;
  }*/

  public GameObject GiveWeaponFromPrefab(GameObject weaponPrefab){
    GameObject player = GameMaster.Instance.Player;
    GameObject weapon = weaponPrefab.Clone(new CloneConfig(new Transform(player.WorldPosition), player, false));
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
    AddWeapon(weapon);
    return weapon;
  }

  // @@TODO remove weapon
  // @@TODO remove boon
}
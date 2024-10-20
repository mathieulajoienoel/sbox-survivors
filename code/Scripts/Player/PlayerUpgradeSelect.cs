public sealed class PlayerUpgradeSelect : Component {
  [RequireComponent] private PlayerMaster master { get; set; }

  [Property] private GameObject LevelUpScreen { get; set; }

	protected override void OnEnabled()
	{
    master.EventLevelUp += OnLevelUp;
    master.EventUpgradeSelect += OnUpgradeSelect;
	}
  protected override void OnDisabled()
	{
    master.EventLevelUp -= OnLevelUp;
    master.EventUpgradeSelect -= OnUpgradeSelect;
	}

  private void OnLevelUp(int level){
    // Generate list of possible upgrades
      //UpgradeBoon
      //UpgradeWeapon
    // Send list to level up screen
    // Open Level up screen
    LevelUpScreen.Enabled = true;
  }

  private void OnUpgradeSelect(GameObject upgrade) {
    // if upgrade is boon
    UpgradeBoon boon = upgrade.Components.Get<UpgradeBoon>();
    if(boon != null){
      AddBoon(boon);
      return;
    }
    // if upgrade is weapon
    UpgradeWeapon upgradeWeapon = upgrade.Components.Get<UpgradeWeapon>();
    if(upgradeWeapon != null){
      AddUpgradeWeapon(upgradeWeapon);
      return;
    }
  }

  private void AddUpgradeBoon(UpgradeBoon boon){
    // Get the boon from the upgrade
    // Apply the boon
    // add to boon list

    // @@TODO update game stats ?
  }
  private void AddUpgradeWeapon(UpgradeWeapon weapon){
    // Get the weapon prefab from the upgrade
    master.Equipment.GiveWeaponFromPrefab(weapon.WeaponPrefab);

    // @@TODO update game stats ?
  }
}
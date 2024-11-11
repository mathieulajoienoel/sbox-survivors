public sealed class PlayerUpgradeSelect : Component {
  [RequireComponent] private PlayerMaster master { get; set; }
  [Property] public GameObject BoonListObject { get; set; }

	//protected override void OnEnabled()
	//{
  //  //master.EventLevelUp += OnLevelUp;
  //  master.EventUpgradeSelect += OnUpgradeSelect;
	//}
  //protected override void OnDisabled()
	//{
  //  //master.EventLevelUp -= OnLevelUp;
  //  master.EventUpgradeSelect -= OnUpgradeSelect;
	//}

  //private void OnUpgradeSelect(UpgradeBoon upgrade) {
  //  // if upgrade is boon
  //  //UpgradeBoon boon = upgrade.Components.Get<UpgradeBoon>();
  //  //if(boon != null){
  //  //  AddUpgradeBoon(boon);
  //  //  return;
  //  //}
  //  // if upgrade is weapon
  //  UpgradeWeapon upgradeWeapon = upgrade.Components.Get<UpgradeWeapon>();
  //  if(upgradeWeapon != null){
  //    AddUpgradeWeapon(upgradeWeapon);
  //    return;
  //  }
  //  // others ?
  //}

  private void AddUpgradeWeapon(UpgradeWeapon weapon){
    // Get the weapon prefab from the upgrade
    master.Equipment.GiveWeaponFromPrefab(weapon.WeaponPrefab);

    // @@TODO update game stats ?
  }
}
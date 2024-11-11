using System.Diagnostics;
using System.IO;
public sealed class UpgradePool : Component {
  private static UpgradePool instance = null;
  static UpgradePool() {
  }
  public static UpgradePool Instance {
    get { return instance; }
  }
  protected override void OnEnabled() {
    instance = this;
    LoadUpgrades();
  }


  [Property] public List<UpgradeBoon> AvailableBoons { get; set; }
  [Property] public List<UpgradeWeapon> AvailableWeapons { get; set; }
  [Property] public List<UpgradeBoon> UsedBoons { get; set; }
  [Property] public List<UpgradeWeapon> UsedWeapons { get; set; }

	private void LoadUpgrades()
	{
    // Load all boons
    //Boons = JsonLoader.Load<UpgradeBoon>("data/Game/UpgradeBoon/*.json");
    AvailableBoons = new List<UpgradeBoon>(){
      new Health1(),
      new Health2()
    };
    // Load all weapons
    //Weapons = JsonLoader.Load<UpgradeWeapon>("data/Game/UpgradeWeapon/*.json");
    AvailableWeapons = new List<UpgradeWeapon>(){

    };
	}



}
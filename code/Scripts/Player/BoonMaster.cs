public sealed class BoonMaster : Component {
  private PlayerMaster master { get; set; }
  [Property] List<UpgradeBoon> Boons { get; set; } = new List<UpgradeBoon>();
  protected override void OnEnabled(){
    master = Components.GetInParent<PlayerMaster>();
    master.EventUpgradeSelect += OnUpgradeSelect;
  }
  protected override void OnDisabled()
	{
    master.EventUpgradeSelect -= OnUpgradeSelect;
	}

  public void OnUpgradeSelect(UpgradeBoon boon){

    // Apply the boon
    //@@TODO

    // add to boon list
    Boons.Add(boon);
  }

}
public sealed class PlayerMaster : EntityMaster
{
  [RequireComponent] public new PlayerStats Stats { get; set; }
  [RequireComponent] public PlayerEquipment Equipment { get; set; }

  [Property] public BoonMaster Boons { get; set; }

	protected override void OnEnabled()
	{
		base.OnEnabled();

    Boons = GetComponentInChildren<BoonMaster>();
	}

	public delegate void CollectItemEventHandler(Item item);
  public event CollectItemEventHandler EventCollectItem;
  public void CallEventCollectItem(Item item){
		EventCollectItem?.Invoke( item );
	}

  public delegate void GainExperienceEventHandler(float value);
  public event GainExperienceEventHandler EventGainExperience;
  public void CallEventGainExperience(float value){
		EventGainExperience?.Invoke( value );
	}

  public delegate void LevelUpEventHandler(int level);
  public event LevelUpEventHandler EventLevelUp;
  public void CallEventLevelUp(int level){
		EventLevelUp?.Invoke( level );
	}

  public delegate void CollectWeaponEventHandler(GameObject weapon);
  public event CollectWeaponEventHandler EventCollectWeapon;
  public void CallEventCollectWeapon(GameObject weapon){
		EventCollectWeapon?.Invoke( weapon );
	}

  public delegate void UpgradeSelectEventHandler(UpgradeBoon upgrade);
  public event UpgradeSelectEventHandler EventUpgradeSelect;
  public void CallEventUpgradeSelect(UpgradeBoon upgrade){
    EventUpgradeSelect?.Invoke(upgrade);
  }
}
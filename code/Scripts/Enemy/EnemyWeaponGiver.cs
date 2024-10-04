public sealed class EnemyWeaponGiver : Component {
	[RequireComponent] private EnemyMaster master { get; set; }
  [Property] public GameObject[] Weapons { get; set; }
	public bool HasWeapon = false;

	protected override void OnEnabled()
	{
		HasWeapon = master.FixedWeaponHolster.Children.Any() || master.AimedWeaponHolster.Children.Any();
		if(!HasWeapon) GiveWeapon();
	}

	public void GiveWeapon(){
		// Select a random weapon and give it to the enemy
		GameObject weapon = Weapons[GameMaster.Instance.Rand(0,Weapons.Length - 1)].Clone(new CloneConfig(new Transform(WorldPosition), GameObject, false));
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
		weapon.WorldPosition = WorldPosition;
		weapon.Enabled = true;

		HasWeapon = true;
		this.Enabled = false;
	}

	public override void Reset()
	{

	}
}
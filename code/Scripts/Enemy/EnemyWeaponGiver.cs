public sealed class EnemyWeaponGiver : Component {
  [Property] public GameObject[] Weapons { get; set; }
	public bool HasWeapon = false;

	protected override void OnEnabled()
	{
		if(!HasWeapon) GiveWeapon();
	}

	public void GiveWeapon(){
		// Select a random weapon and give it to the enemy
		GameObject weapon = Weapons[GameMaster.Instance.Rand(0,Weapons.Length - 1)].Clone(new CloneConfig(new Transform(), GameObject, false));
		weapon.SetParent(GameObject);
		weapon.Transform.Position = Transform.Position;
		weapon.Enabled = true;

		HasWeapon = true;
		this.Enabled = false;
	}
}

public sealed class PlayerStats : EntityStats
{
	[Property] public float Experience { get; set; } = 0f;
	[Property] public float ExperienceNeeded { get; set; } = 3f;
	[Property] public int Level { get; set; } = 1;
	[Property] public float CollectRadius { get; set; } = 50f;
	[Property] public int MaxWeapons { get; set; } = 6;
  [Property] public int MaxBoons { get; set; } = 6;
	[Property] public int PossibleUpgradesOffered { get; set; } = 3;


	//[RequireComponent] private PlayerMaster master { get; set; }

	/*protected override void OnAttributeChange(Dictionary<string, float> AttributesValues){
		// @@TODO change attributes ?
		switch (switch_on)
		{

			default:
		}

		master.CallEventAttributesChanged();
	}*/
}

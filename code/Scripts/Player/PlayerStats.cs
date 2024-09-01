
public sealed class PlayerStats : EntityStats
{
	[Property] public float Experience { get; set; } = 0f;
	[Property] public float ExperienceNeeded { get; set; } = 10f;
	[Property] public int Level { get; set; } = 1;
	[Property] public float CollectRadius { get; set; } = 50f;
}

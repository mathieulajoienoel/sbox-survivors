
public sealed class PlayerStats : EntityStats
{
	[Property] public float Experience { get; set; } = 0f;
	[Property] public float CollectRadius { get; set; } = 50f;
}

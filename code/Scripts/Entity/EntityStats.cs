
public abstract class EntityStats : Component, IEntityStats
{
	[Property] public bool Alive { get; set; } = true;
	[Property] public float MaxHealth { get; set; } = 100f;
	[Property] public float MoveSpeed { get; set; } = 25;
	[Property] public float HealthRegenerationRate { get; set; } = 0f;
	[Property] public float InvincibilityTime { get; set; } = 0.01f;
}

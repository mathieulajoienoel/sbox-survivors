
public abstract class EntityStats : Component, IEntityStats
{
	[Property] public bool Alive { get; set; } = true;
	[Property] public float MaxHealth { get; set; } = 100f;
	[Property] public float MoveSpeed { get; set; } = 25;

}

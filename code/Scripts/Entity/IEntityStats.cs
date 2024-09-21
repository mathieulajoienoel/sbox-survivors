public interface IEntityStats {
  bool Alive { get; set; }
  float MaxHealth { get; set; }
  float MoveSpeed { get; set; }
  float HealthRegenerationRate { get; set; }
  float InvincibilityTime { get; set; }
}
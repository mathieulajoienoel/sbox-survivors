public sealed class WeaponArea : PlayerWeaponBase {
  [Property] protected override float Damage { get; set; } = -5f;
  [Property] protected override float Cooldown { get; set; } = 0.75f;
  [Property] protected override Vector3 Range { get; set; } = new Vector3(1.1f,1.1f,0.5f);
  [Property] protected override int MaxTargets { get; set; } = 2;
  [Property] protected override Vector3 Size { get; set; } = new Vector3(1.1f,1.1f,0.5f);
  [Property] protected override Vector3 Position { get; set; } = new Vector3(0,0,0);
  [Property] protected override Angles StartRotation { get; set; } = new Angles(0,0,0);
  [Property] protected override int Speed { get; set; } = 0;
  [Property] protected override int SubWeaponSpeed { get; set; } = 0;
}
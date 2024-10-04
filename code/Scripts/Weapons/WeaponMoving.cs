public sealed class WeaponMoving : PlayerWeaponBase {
  [Property] protected override float Damage { get; set; } = -15f;
  [Property] protected override float Cooldown { get; set; } = 0.5f;
  [Property] protected override Vector3 Range { get; set; } = new Vector3(100f,0,0f);
  [Property] protected override Vector3 Size { get; set; } = new Vector3(0.15f, 0.15f, 0.15f); //new Vector3(0.1f, 0.1f, 0.25f);
  [Property] protected override Vector3 Position { get; set; } = new Vector3(0,0,0);
  [Property] protected override Angles StartRotation { get; set; } = new Angles(0,0,0);
  [Property] protected override int MaxTargets { get; set; } = -1;
  [Property] protected override int Speed { get; set; } = 150;
  [Property] protected override int SubWeaponSpeed { get; set; } = 0;
  [Property] protected override float Knockback { get; set; } = 50000f;
  [Property] protected override float KnockbackDuration { get; set; } = 1f;

  protected override void ApplyStartingRotation(){
   GameObject.Parent.LocalRotation = Rotation.From(StartRotation);
  }
  protected override void ApplySize(){
    LocalScale = Size;
  }
  protected override void ApplyRange(){
    LocalPosition = Range;
  }

  protected override void ApplySpeed(){
    MoveTranslate movingCmp = Components.Get<MoveTranslate>();
    movingCmp.Speed = Speed;
    movingCmp.Step = Speed;
    movingCmp.Length = Range;
  }
}
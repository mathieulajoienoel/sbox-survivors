public abstract class Rotator : Component {
	[Property] public float Speed { get; set; }
  [Property] public Vector3 Step { get; set; }
  protected void Rotate(){
    LocalRotation = Rotation.Slerp(LocalRotation, new Angles(
     0,
     Step.y * Time.Now,
     0
    ), Time.Delta * Speed);
  }
}
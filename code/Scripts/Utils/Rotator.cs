public abstract class Rotator : Component {
	[Property] public float Speed { get; set; }
  [Property] public Vector3 Step { get; set; }
  protected void Rotate(){
    Transform.LocalRotation = Rotation.Slerp(Transform.LocalRotation, new Angles(
     0,
     Step.y * Time.Now,
     0
    ), Time.Delta * Speed);
  }
}
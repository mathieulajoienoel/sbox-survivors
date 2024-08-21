public sealed class EntityCharacter : Component
{
	public void RotateTo(float rotY){
    Transform.LocalRotation = Rotation.Slerp(Transform.LocalRotation, new Angles(0, rotY, 0), Time.Delta * 25f);
  }
}
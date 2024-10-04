public sealed class EntityCharacter : Component
{
	public void RotateTo(float rotY){
    LocalRotation = Rotation.Slerp(LocalRotation, new Angles(0, rotY, 0), Time.Delta * 25f);
  }
}
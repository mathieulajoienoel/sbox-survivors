public sealed class FollowEntity : Component, Component.ITriggerListener {
  [Property] public float Force = 1f;
  [Property] public float Speed = 1f;
  [Property] public TagSet AttractedTags;
  [Property] public float Size = 400f;
  private Collider Target;

	protected override void OnEnabled()
	{
		SphereCollider collider = Components.Get<SphereCollider>();
    collider.Radius = Size;
	}

	public void OnTriggerEnter( Collider other )
  {
    if(Target != null || !HasAttractedTag(other.GameObject.Tags)) return;
    Target = other;
  }
  public void OnTriggerExit( Collider other )
  {
    if(Target == null || !HasAttractedTag(other.GameObject.Tags)) return;
    Target = null;
  }

  private bool HasAttractedTag(GameTags tags){
    foreach (var tag in tags.ToArray())
    {
      if(AttractedTags.Contains(tag)) return true;
    }
    return false;
  }

	protected override void OnUpdate()
  {
      if(Target != null){
        OnTriggerUpdate(Target);
      }
  }
  private void OnTriggerUpdate(Collider item){
    Vector3 position = item.GameObject.WorldPosition;
    Vector3 direction = (position - WorldPosition).Normal;
    Force *= 1.1f;
    GameObject.Parent.WorldPosition = GameObject.Parent.WorldPosition.LerpTo(position + direction * Force, Time.Delta * Speed);
  }
}
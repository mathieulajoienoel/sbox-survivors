public sealed class FollowEntity : Component, Component.ITriggerListener {
  [Property] public float Force { get; set; } = 1f;
  [Property] public float Speed { get; set; } = 1f;
  [Property] public TagSet AttractedTags { get; set; } = new TagSet();
  [Property] public float Size { get; set; } = 400f;
  private Collider Target { get; set; }

	protected override void OnEnabled()
	{
    AttractedTags ??= new TagSet();
    if(AttractedTags.IsEmpty){
      AttractedTags.Add("player");
    }
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
    if(!Enabled || AttractedTags.IsEmpty) return false;
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
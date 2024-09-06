public sealed class WeaponBaseProjectile : Component, Component.ICollisionListener {
	public EntityMaster master { get; set; }
  [Property] public DamageInfo Damage { get; set; }
	[Property] public float Speed { get; set; }
	[Property] public Vector3 Direction { get; set; }

	protected override void OnEnabled()
	{
		Transform.Rotation = Rotation.LookAt(Direction);
	}

  protected override void OnDisabled()
	{
	}

	protected override void OnFixedUpdate()
	{
		Vector3 position = Transform.Position.LerpTo(Transform.Position + (Speed * Transform.Rotation.Forward) + 1, Time.Delta);
		position.z = 12.5f;
		Transform.Position = position;
	}

	public void OnCollisionStart(Collision collision)
  {
		EntityMaster otherMaster = collision.Other.GameObject.Components.GetInAncestorsOrSelf<EntityMaster>();
		if(otherMaster != null){
			otherMaster.CallEventReceiveDamage(Damage);
			master.CallEventDealDamage(Damage);
		}

    GameObject.Destroy();
  }

}
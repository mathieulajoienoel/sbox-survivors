public sealed class WeaponBaseProjectile : Component, Component.ICollisionListener {
	public EntityMaster master { get; set; }
  [Property] public DamageInfo Damage { get; set; }
	[Property] public float Speed { get; set; }
	[Property] public Vector3 Direction { get; set; }
	[Property] public float Duration { get; set; } = 5;
	private float DeleteAt { get; set; }

	public void ApplyAttributes(EntityMaster attrMaster, DamageInfo attrDamage, float attrSpeed, Vector3 attrDirection, float attrDuration){
		master = attrMaster;
		Damage = attrDamage;
		Speed = attrSpeed;
		Direction = attrDirection;
		Duration = attrDuration;

		GameObject.Enabled = true;
	}

	protected override void OnEnabled()
	{
		Transform.Rotation = Rotation.LookAt(Direction);
		DeleteAt = Time.Now + Duration;
	}

  protected override void OnDisabled()
	{
	}

	protected override void OnFixedUpdate()
	{
		Vector3 position = Transform.Position.LerpTo(Transform.Position + (Speed * Transform.Rotation.Forward) + 1, Time.Delta);
		position.z = 12.5f;
		Transform.Position = position;
		if(Time.Now > DeleteAt){
			GameObject.Destroy();
		}
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
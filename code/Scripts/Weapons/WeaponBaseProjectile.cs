public sealed class WeaponBaseProjectile : Component, Component.ICollisionListener {
	public EntityMaster master { get; set; }
  [Property] public DamageInfo Damage { get; set; }
	[Property] public float Speed { get; set; }
	[Property] public Vector3 Direction { get; set; }
	[Property] public float Duration { get; set; } = 5;
	private float DeleteAt { get; set; }
	public string ProjectilePool { get; set; }

	public void ApplyAttributes(EntityMaster attrMaster, DamageInfo attrDamage, float attrSpeed, Vector3 attrDirection, float attrDuration, string projectilePool){
		master = attrMaster;
		Damage = attrDamage;
		Speed = attrSpeed;
		Direction = attrDirection;
		Duration = attrDuration;
		ProjectilePool = projectilePool;

		ModelRenderer renderer = Components.Get<ModelRenderer>(true);
		renderer.Model = Model.Cube;
		renderer.Tint = Color.White;

		GameObject.Enabled = true;
	}

	protected override void OnEnabled()
	{
		WorldRotation = Rotation.LookAt(Direction);
		DeleteAt = Time.Now + Duration;
	}

  protected override void OnDisabled()
	{
	}

	protected override void OnFixedUpdate()
	{
		Vector3 position = WorldPosition.LerpTo(WorldPosition + (Speed * WorldRotation.Forward) + 1, Time.Delta);
		position.z = 12.5f;
		WorldPosition = position;
		if(Time.Now > DeleteAt){
			ReturnToPool();
		}
	}

	public void OnCollisionStart(Collision collision)
  {
		EntityMaster otherMaster = collision.Other.GameObject.Components.GetInAncestorsOrSelf<EntityMaster>();
		if(otherMaster != null){
			otherMaster.CallEventReceiveDamage(Damage);
			master.CallEventDealDamage(Damage);
		}

		ReturnToPool();
  }

	public void ReturnToPool(){
		//GameObject.Enabled = false;
		ObjectPool.Instance.ReturnToPool(ProjectilePool, GameObject);
	}

}
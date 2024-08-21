public sealed class FadeOutAfterTime : Component {
  [Property] public float TimeToWait = 1f;
  [Property] public float DestroyAfter = 3f;
  [Property] public float Speed = 10f;
  private float WaitTo = 0.0f;
  private float DestroyAt = 0.0f;
  protected override void OnEnabled() {
    WaitTo = Time.Now + TimeToWait;
    DestroyAt = WaitTo + DestroyAfter;
  }

  protected override void OnFixedUpdate() {
    if(Time.Now > WaitTo) {
      Transform.Scale = Transform.Scale.LerpTo(Transform.Scale - Transform.Scale * 0.1f, Time.Delta * Speed);
    }

    if(Time.Now > DestroyAt){
      GameObject.Destroy();
    }
  }
}
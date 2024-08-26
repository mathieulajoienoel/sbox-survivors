public sealed class DestroyAfterTime : Component {
  [Property] public float TimeToWait = 1.5f;
  private float Done = 0.0f;
  protected override void OnEnabled() {
    Done = Time.Now + TimeToWait;
  }

  protected override void OnUpdate() {
    if(Time.Now > Done) {
      GameObject.Destroy();
    }
  }
}
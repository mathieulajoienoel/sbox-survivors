public sealed class PopupFadeOutAfterTime : Component {
  [Property] public float TimeToWait = 1f;
  [Property] public float DestroyAfter = 0.25f;
  [Property] public float Speed = 10f;
  private float WaitTo = 0.0f;
  private float DestroyAt = 0.0f;
  protected override void OnEnabled() {
    WaitTo = Time.Now + TimeToWait;
    DestroyAt = WaitTo + DestroyAfter;
  }

  public void SetTimings(float timeToWait, float destroyAfter, float speed){
    TimeToWait = timeToWait;
    DestroyAfter = destroyAfter;
    Speed = speed;

    WaitTo = Time.Now + TimeToWait;
    DestroyAt = WaitTo + DestroyAfter;
  }

  protected override void OnUpdate() {
    if(Time.Now > WaitTo) {
      WorldScale = WorldScale.LerpTo(WorldScale - WorldScale * 0.1f, Time.Delta * Speed);
    }

    if(Time.Now > DestroyAt){
      InfoPopup popup = Components.Get<InfoPopup>();
      if(popup == null) {
        GameObject.Destroy();
        return;
      }
      ObjectPool.Instance.ReturnToPool(popup.PopupPool, GameObject);
    }
  }
}
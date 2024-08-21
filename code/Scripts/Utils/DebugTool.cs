public abstract class DebugTool : Component {
  protected override void OnEnabled()
	{
		base.OnEnabled();
    if(GameMaster.Instance == null || !GameMaster.Instance.DebugMode){
      Destroy();
      return;
    }

    OnDebug();
	}

  protected abstract void OnDebug();
}
public sealed class PlayerMaster : EntityMaster
{
  public new PlayerStats Stats { get; set; }

  public delegate void CollectItemEventHandler(Item item);
  public event CollectItemEventHandler EventCollectItem;
  public void CallCollectItem(Item item){
		EventCollectItem?.Invoke( item );
	}

  public delegate void GainExperienceEventHandler(float value);
  public event GainExperienceEventHandler EventGainExperience;
  public void CallGainExperience(float value){
		EventGainExperience?.Invoke( value );
	}

  protected override void OnAwake(){
    base.OnAwake();
    Stats = Components.Get<PlayerStats>();
  }
}
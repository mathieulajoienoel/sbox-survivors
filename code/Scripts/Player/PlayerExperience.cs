using System;
public sealed class PlayerExperience : Component, Component.ITriggerListener {
  private PlayerMaster master;
  protected override void OnEnabled(){
    master = Components.GetInParentOrSelf<PlayerMaster>();

    SetCollectRadius();

    master.EventCollectItem += OnCollectItem;
    master.EventGainExperience += OnExperienceGain;
    master.EventGainExperience += ShowExperiencePopup;
  }

  private void SetCollectRadius(){
    float radius = master.GameObject.Components.Get<PlayerStats>().CollectRadius;
    SphereCollider collider = Components.Get<SphereCollider>();
    collider.Radius = radius;
  }

  protected override void OnDisabled()
	{
		base.OnDisabled();
    master.EventCollectItem -= OnCollectItem;
    master.EventGainExperience -= OnExperienceGain;
    master.EventGainExperience -= ShowExperiencePopup;
	}

  private void OnCollectItem(Item item){
    if(item.Type != CollectableType.Experience) return;
    if(item.Value == 0) return;
    master.CallGainExperience(item.Value);
  }

  private void OnExperienceGain(float value){
    master.Stats.Experience += value;
  }

  private void ShowExperiencePopup(float value){
    Vector3 position = master.GameObject.Transform.Position;
    position.z = 15;
    position += new Vector3(GameMaster.Instance.Rand(-20,20),GameMaster.Instance.Rand(-20,20), 0);
    GameObject experiencePopup = GameMaster.Instance.ExperiencePopupPrefab.Clone(position);
    experiencePopup.Components.Get<ExperiencePopup>().Display(value);
  }

  public void OnTriggerEnter( Collider other )
	{
		if(!other.GameObject.Tags.Has("collectable")) return;
    Experience experience = other.GameObject.Components.Get<Experience>();
    if(experience == null) return;
    Item item = experience.Collect();
    if(item == null) return;

    master.CallCollectItem(item);
	}
}
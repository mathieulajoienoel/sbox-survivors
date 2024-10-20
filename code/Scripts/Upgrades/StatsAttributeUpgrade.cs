using Sandbox.UI;

public abstract class StatsAttributeUgrade : Component, IAttributeUpgrade {
  [Property] public string Name { get; set; } // The name of the upgrade
  [Property] public Image Image { get; set; } // Image to show
  [Property] public string Description { get; set; } // Short description of the upgrade
  [Property] public int Cost { get; set; } // Cost of the attribute upgrade
  [Property] public AttributeUpgradeRarity Rarity { get; set; } // Upgrade rarity, lower is more common
  [Property] public ModifiableComponent TargetedComponent { get; set; } // Which component to target
  [Property] public string TargetedComponentName { get; set; } // Component name to target
  [Property] public string TargetedAttribute { get; set; } // Which attribute to target
  [Property] public float Value { get; set; } // Value to apply
  [Property] public float Multiplier { get; set; } // Multiplier to apply to the value
  [Property] public bool IsValuePercentage { get; set; } // Is the value a percentage (x%) ?

  protected override void OnEnabled(){
    TargetedComponent = GameMaster.Instance.Player.Components.Get<PlayerMaster>().Stats;
  }

  // Apply the value to the component's attribute.
  public void ApplyValueToComponentAttribute() {
    /*float OriginalValue = (float)TargetedComponent.GetProperty(TargetedAttribute).GetValue();

    Dictionary<string, float> params = new Dictionary<string, float>();
    params.Add(TargetedAttribute, this.CalculateValue(float OriginalValue));

    TargetedComponent.CallEventAttributeChange(params);
    this.Enabled = false;*/
  }

  // Calculate the final Value
  public float CalculateValue(float OriginalValue){
    float val = Value;
    if (IsValuePercentage) {
      val = Value * OriginalValue / 100;
    }
    return val * Multiplier * (float)Rarity;
  }
}
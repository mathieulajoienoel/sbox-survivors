using Sandbox.UI;
public sealed class Health1 : UpgradeBoon {
  [Property] public override string Name { get; set; } = "Health 1"; // The name of the upgrade
  [Property] public override Image Image { get; set; } = null; // Image to show
  [Property] public override string Description { get; set; } = "+1 max health"; // Short description of the upgrade
  [Property] public override int Cost { get; set; } = 0; // Cost of the attribute upgrade
  [Property] public override AttributeUpgradeRarity Rarity { get; set; } = AttributeUpgradeRarity.Common; // Upgrade rarity, lower is more common
  [Property] public override string TargetedObjectName { get; set; } = "Player"; // Which GameObject to target
  [Property] public override string TargetedComponentName { get; set; } = "PlayerStats"; // Component name to target
  [Property] public override string TargetedAttribute { get; set; } = "MaxHealth"; // Which attribute to target
  [Property] public override float Value { get; set; } = 1; // Value to apply
  [Property] public override float Multiplier { get; set; } = 1; // Multiplier to apply to the value
  [Property] public override string ComputedValue { get; set; } = ""; // Final computed value; leave empty
  [Property] public override bool IsValuePercentage { get; set; } = false; // Is the value a percentage (x%) ?
  [Property] public override StatsAttributeUpgrade Prerequisite { get; set; } = null; // Other upgrade required before using this one

  public override float GetOriginalValue(){  // Get the original value from the component
    if(TargetedComponent == null) GetTargetedComponent();
    return TargetedComponent.MaxHealth;
  }

  public override void ApplyValueToComponentAttribute(){
    if(TargetedComponent == null) GetTargetedComponent();
    TargetedComponent.MaxHealth = this.CalculateValue();
    playerMaster.CallEventAttributesChanged();
  }
}
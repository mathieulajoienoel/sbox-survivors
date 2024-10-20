using Sandbox.UI;

public interface IAttributeUpgrade {
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
  public abstract void ApplyValueToComponentAttribute(); // Apply the value to the component's attribute.
	protected float CalculateValue(float OriginalValue); // Calculate the final Value
}


public enum AttributeUpgradeRarity {
  Common = 1,
  Uncommon = 2,
  Rare = 3,
  Legendary = 4,
  Godly = 5
}
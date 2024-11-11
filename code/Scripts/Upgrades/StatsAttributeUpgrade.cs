using System;
using System.Reflection;
using Sandbox.UI;

public abstract class StatsAttributeUpgrade : Component, IAttributeUpgrade {
  [Property] public abstract string Name { get; set; } // The name of the upgrade
  [Property] public abstract Image Image { get; set; } // Image to show
  [Property] public abstract string Description { get; set; } // Short description of the upgrade
  [Property] public abstract int Cost { get; set; } // Cost of the attribute upgrade
  [Property] public abstract AttributeUpgradeRarity Rarity { get; set; } // Upgrade rarity, lower is more common
  [Property] public abstract string TargetedObjectName { get; set; } // Which GameObject to target
  [Property] public virtual PlayerStats TargetedComponent { get; set; } // Which component to target
  [Property] public virtual PlayerMaster playerMaster { get; set; } // The component's master
  [Property] public abstract string TargetedComponentName { get; set; } // Component name to target
  [Property] public abstract string TargetedAttribute { get; set; } // Which attribute to target
  [Property] public abstract float Value { get; set; } // Value to apply
  [Property] public abstract float Multiplier { get; set; } // Multiplier to apply to the value
  [Property] public abstract string ComputedValue { get; set; } // Final computed value; leave empty
  [Property] public abstract bool IsValuePercentage { get; set; } // Is the value a percentage (x%) ?
  [Property] public abstract StatsAttributeUpgrade Prerequisite { get; set; } // Other upgrade required before using this one

  public virtual string GetRarity(){
    return Rarity.ToString();
  }

  public virtual void GetTargetedComponent(){
    playerMaster = GameMaster.Instance.Player.Components.Get<PlayerMaster>();
    TargetedComponent = playerMaster.Stats;
  }

  // Apply the value to the component's attribute.
  public virtual void ApplyValueToComponentAttribute() {
    /*float OriginalValue = (float)TargetedComponent.GetProperty(TargetedAttribute).GetValue();

    Dictionary<string, float> params = new Dictionary<string, float>();
    params.Add(TargetedAttribute, this.CalculateValue(float OriginalValue));

    TargetedComponent.CallEventAttributeChange(params);
    this.Enabled = false;*/
  }

  public float CalculateValue(){
    //Type thisType = TargetedComponent.GetType();
    //MethodInfo theMethod = thisType.GetMethod(TargetedAttribute);
    //float OriginalValue = (float)theMethod.Invoke(TargetedComponent, Array.Empty<object>());
    float OriginalValue = GetOriginalValue();

    float val = Value;
    if (IsValuePercentage) {
      val = Value * OriginalValue / 100;
    }
    return val * Multiplier * (float)Rarity;
  }

  // Calculate the final Value
  public void PrepareDisplayValue(){
    float newValue = CalculateValue();
    if (IsValuePercentage) {
      ComputedValue = newValue.ToString();
    } else {
      ComputedValue = newValue.ToString() + "%";
    }
  }

  public abstract float GetOriginalValue(); // Get the original value from the component
}
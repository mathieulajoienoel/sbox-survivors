public abstract class ModifiableComponent : Component {
  public delegate void AttributeChangeEventHandler(Dictionary<string, float> AttributesValues);
  public event AttributeChangeEventHandler EventAttributeChange;
  public void CallEventAttributeChange(Dictionary<string, float> AttributesValues){
		EventAttributeChange?.Invoke(AttributesValues);
	}

  protected abstract void OnAttributeChange(Dictionary<string, float> AttributesValues);
}

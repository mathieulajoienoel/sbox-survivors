public sealed class Item : Component {
  [Property] public CollectableType Type;
  [Property] public float Value = 0f;
  [Property] public GameObject item { get; set; }
}
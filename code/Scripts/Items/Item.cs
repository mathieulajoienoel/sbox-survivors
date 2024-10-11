public sealed class Item : Component {
  [Property] public CollectableType Type { get; set; }
  [Property] public float Value { get; set; } = 0f;
  [Property] public GameObject item { get; set; }
}
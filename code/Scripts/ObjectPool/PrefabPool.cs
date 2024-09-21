public struct PrefabPool {
  [Property] public GameObject Prefab { get; set; }
  [Property] public int Amount { get; set; }
  public Stack<GameObject> InstanciatedPrefabs { get; set; }
  public List<GameObject> LoanedPrefabs { get; set; }
}
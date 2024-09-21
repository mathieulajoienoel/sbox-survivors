public sealed class ObjectPool : Component {
  private static ObjectPool instance = null;
  static ObjectPool() {
  }
  public static ObjectPool Instance {
    get { return instance; }
  }
  [Property] public Vector3 ResetLocation { get; set; } = Vector3.Zero;
  [Property] public Dictionary<string, PrefabPool> PrefabPools { get; set; }

  protected override void OnAwake()
	{
    instance = this;
    InstanciatePool();
	}

  private void InstanciatePool(){
    CloneConfig cloneConfig = new CloneConfig(new Transform(Transform.Position), GameObject, false);
    foreach (PrefabPool pool in PrefabPools.Values)
    {
      for (int i = 0; i < pool.Amount; i++)
      {
        pool.InstanciatedPrefabs.Push(pool.Prefab.Clone(cloneConfig));
      }
    }
  }

  public GameObject GetObjectFromPool(string prefabPoolName){
    PrefabPool pool = PrefabPools[prefabPoolName];
    GameObject prefab = pool.InstanciatedPrefabs.Pop();
    pool.LoanedPrefabs.Add(prefab);
    return prefab;
  }

  public void ReturnToPool(string prefabPoolName, GameObject obj) {
    PrefabPool pool = PrefabPools[prefabPoolName];
    pool.LoanedPrefabs.Remove(obj);
    Resetter resetter = obj.Components.Get<Resetter>();
    if(resetter == null){
      Log.Error("Object does not have a Resetter component");
      Log.Error(obj);
      return;
    }
    GameObject resettedObject = resetter.ResetAll();
    resettedObject.Transform.Position = ResetLocation;
    pool.InstanciatedPrefabs.Push(resettedObject);
  }
}
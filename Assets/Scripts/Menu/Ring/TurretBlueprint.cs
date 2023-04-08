using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public Texture[] icons;

    public int[] prices = { 100, 200, 1000 };
    //public GameObject TurretBasePrefab;
    public GameObject[] TurretPrefabs;
    //public GameObject prefab;

    //public GameObject getPrefab()
    //{
    //    return TurretPrefabs[RadialMenu.node.LevelTurret];
    //}
}
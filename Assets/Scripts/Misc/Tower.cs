using UnityEngine;

[System.Serializable]
public class Tower
{
    [SerializeField]
    private Texture[] icons;

    [SerializeField]
    private int[] prices;

    [SerializeField]
    private GameObject[] towersPrefabs;

    public Texture[] GetIcons()
    {
        return icons;
    }

    public int[] GetPrices()
    {
        return prices;
    }

    public GameObject[] GetTowersPrefabs()
    {
        return towersPrefabs;
    }
}
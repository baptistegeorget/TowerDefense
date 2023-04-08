using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public Texture icon;
    public int price;
    public GameObject prefab;

    public GameObject getPrefab()
    {
        return prefab;
    }

    public Texture getIcon()
    {
        return icon;
    }

    public int getPrice()
    {
        return price;
    }
}

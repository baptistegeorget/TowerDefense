using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[System.Serializable]
public class TurretBlueprint
{


    public Texture icon;

    public int price;

    public GameObject prefeb;

    public GameObject getPrefab()
    {
        return prefeb;
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



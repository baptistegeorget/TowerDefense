using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RadialMenu : MonoBehaviour
{
    public TurretBlueprint[] listTurret;

    private GameObject turret;
    public Vector3 positionOffset = new Vector3(0, 1.11f, 0);

    public GameObject EntryPrefab;

    private float Radius = 200;

    private RawImage targetIcon;

    private List<RadialMenuEntry> Entries;

    private Transform node;

    void Start()
    {
        Entries = new List<RadialMenuEntry>();
    }

    void AddEntry(string plabel, Texture pIcon, GameObject pPrefab, RadialMenuEntry.RingMenuEntryDelegete pCallback)
    {
        GameObject entry = Instantiate(EntryPrefab, transform);
        RadialMenuEntry rme = entry.GetComponent<RadialMenuEntry>();
        rme.Setlabel(plabel);
        rme.SetIcon(pIcon);
        rme.SetPrefab(pPrefab);
        rme.SetCallback(pCallback);
        Entries.Add(rme);
    }

    public void BuildTurret()
    {
            GameObject TurretToBuild = BuildManager.instance.GetTurretToBuild();
            turret = Instantiate(TurretToBuild, node.position + positionOffset, Quaternion.Euler(new Vector3(0, 0, 0)));
            BuildManager.instance.SetUp = false;
    }

    public void Open()
    {
        for (int i = 0; i < listTurret.Length; i++)
        {
            AddEntry(listTurret[i].price.ToString(), listTurret[i].icon, listTurret[i].prefab, SetTargetIcon);
        }
        Rearrange();
    }

    public void Close()
    {
        for (int i = 0; i < listTurret.Length; i++)
        {
            RectTransform rect = Entries[i].GetComponent<RectTransform>();
            GameObject entry = Entries[i].gameObject;
            rect.DOAnchorPos(Vector3.zero, .3f).SetEase(Ease.OutQuad).onComplete = 
                delegate()
                {
                    Destroy(entry);
                };
        }
        Entries.Clear();
    }

    public void  Toggle(Transform nodeTransform)
    {
        if (Entries.Count == 0)
        {
            node = nodeTransform;
            Open();
        }
        else
        {
            Close();
        }
    }

    void Rearrange()
    {
        float radiansOfSeparation = (Mathf.PI * 2) / Entries.Count;
        for (int i = 0; i < Entries.Count; i++)
        {
            float x = Mathf.Sin(radiansOfSeparation * i) * Radius;
            float y = Mathf.Cos(radiansOfSeparation * i) * Radius;
            RectTransform rect = Entries[i].GetComponent<RectTransform>();
            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad).SetDelay(.05f*i);
            rect.DOAnchorPos(new Vector3(x, y, 0), .3f).SetEase(Ease.OutQuad).SetDelay(.05f*i);
        }
    }

    void SetTargetIcon(RadialMenuEntry pEntry)
    {
        targetIcon.texture = pEntry.GetIcon();
    }
}

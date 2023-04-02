using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RadialMenu : MonoBehaviour
{
    public static Node node;

    public TurretBlueprint[] listTurret;
    public GameObject entryPrefab;

    private float radius = 200;
    private List<RadialMenuEntry> entries = new List<RadialMenuEntry>();

    public void Toggle(Node _node)
    {
        if (entries.Count == 0)
        {
            node = _node;
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Open()
    {
        for (int i = 0; i < listTurret.Length; i++)
        {
            AddEntry(listTurret[i].price.ToString(), listTurret[i].icon, listTurret[i].prefab);
        }
        PlaceUI();
    }

    public void Close()
    {
        for (int i = 0; i < entries.Count; i++)
        {
            RectTransform rect = entries[i].GetComponent<RectTransform>();
            GameObject entry = entries[i].gameObject;
            rect.DOAnchorPos(Vector3.zero, 0.3f).SetEase(Ease.OutQuad).onComplete =
                delegate ()
                {
                    Destroy(entry);
                };
        }
        entries.Clear();
    }

    void AddEntry(string label, Texture icon, GameObject prefab)
    {
        GameObject entry = Instantiate(entryPrefab, transform);
        RadialMenuEntry rme = entry.GetComponent<RadialMenuEntry>();
        rme.prefab = prefab;
        rme.icon.texture = icon;
        rme.label.text = label;
        entries.Add(rme);
    }

    void PlaceUI()
    {
        float radiansOfSeparation = (Mathf.PI * 2) / entries.Count;
        for (int i = 0; i < entries.Count; i++)
        {
            float x = Mathf.Sin(radiansOfSeparation * i) * radius;
            float y = Mathf.Cos(radiansOfSeparation * i) * radius;
            RectTransform rect = entries[i].GetComponent<RectTransform>();
            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad).SetDelay(0.05f * i);
            rect.DOAnchorPos(new Vector3(x, y, 0), 0.3f).SetEase(Ease.OutQuad).SetDelay(0.05f * i);
        }
    }
}

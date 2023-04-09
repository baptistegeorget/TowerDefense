using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RadialMenu : MonoBehaviour
{
    private GameObject entryPrefab;
    private float radius;
    private List<RadialMenuEntry> entries;

    private void Start()
    {
        entries = new List<RadialMenuEntry>();
        radius = 200;
    }

    public void Toggle(int levelTower)
    {
        if (levelTower == 0)
        {
            Open();
        } 
        else
        {
            //Amelioration
        }
    }

    public void Open()
    {
        for (int i = 0; i < GameManager.gameManager.towersList.Length; i++)
        {
            AddEntry(GameManager.gameManager.towersList[i].prices[0].ToString(), GameManager.gameManager.towersList[i].icons[0], GameManager.gameManager.towersList[i].towersPrefabs[0]);
        }
        Debug.Log(entries.Count);
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

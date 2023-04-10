using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RadialMenu : MonoBehaviour
{
    public static RadialMenu radialMenu;
    public static bool radialMenuHasOpen = false;

    public GameObject radialItem;
    public Texture deleteIcon;
    public Texture upgradeIcon;
    public Texture cancelIcon;
    public Texture transparent;

    private List<RadialMenuEntry> radialsItems = new List<RadialMenuEntry>();

    private void Start()
    {
        radialMenu = this;
    }

    public void Toggle()
    {
        if (Node.selectedNode.GetLevelTower() == 0)
        {
            OpenBuildMenu();
        } 
        else
        {
            OpenUpgradeMenu();
        }
    }

    private void OpenBuildMenu()
    {
        radialMenuHasOpen = true;
        AddCancelButton();
        for (int i = 0; i < GameManager.gameManager.towersList.Length; i++)
        {
            AddTowerButton(GameManager.gameManager.towersList[i].towersPrefabs[0], GameManager.gameManager.towersList[i].icons[0], GameManager.gameManager.towersList[i].prices[0].ToString());
        }
        PlaceUI();
    }

    private void OpenUpgradeMenu()
    {
        radialMenuHasOpen = true;
        AddCancelButton();
        if (Node.selectedNode.GetLevelTower() < 3)
        {
            AddUpgradeButton();
        }
        AddDeleteButton();
        PlaceUI();
    }

    private void AddTowerButton(GameObject prefab, Texture texture, string text)
    {
        GameObject towerButton = Instantiate(radialItem, transform);
        RadialMenuEntry towerButtonScript = towerButton.GetComponent<RadialMenuEntry>();
        towerButtonScript.prefab = prefab;
        towerButtonScript.icon.texture = texture;
        towerButtonScript.label.text = text;
        radialsItems.Add(towerButtonScript);
    }

    private void AddDeleteButton()
    {
        GameObject deleteButton = Instantiate(radialItem, transform);
        RadialMenuEntry deleteButtonScript = deleteButton.GetComponent<RadialMenuEntry>();
        deleteButtonScript.icon.texture = deleteIcon;
        deleteButtonScript.label.text = "Delete";
        deleteButtonScript.deleteButton = true;
        radialsItems.Add(deleteButtonScript);
    }

    private void AddUpgradeButton()
    {
        GameObject updateButton = Instantiate(radialItem, transform);
        RadialMenuEntry updateButtonScript = updateButton.GetComponent<RadialMenuEntry>();
        updateButtonScript.prefab = FindUpgradePrefab();
        updateButtonScript.icon.texture = upgradeIcon;
        updateButtonScript.label.text = FindUpgradePrice();
        updateButtonScript.upgradeButton = true;
        radialsItems.Add(updateButtonScript);
    }

    private void AddCancelButton()
    {
        GameObject cancelButton = Instantiate(radialItem, transform);
        RadialMenuEntry cancelButtonScript = cancelButton.GetComponent<RadialMenuEntry>();
        cancelButtonScript.icon.texture = cancelIcon;
        cancelButtonScript.label.text = "Cancel";
        cancelButtonScript.cancelButton = true;
        radialsItems.Add(cancelButtonScript);
    }

    public void Close()
    {
        // Erreur DOTween à régler ici
        for (int i = 0; i < radialsItems.Count; i++)
        {
            RectTransform rect = radialsItems[i].GetComponent<RectTransform>();
            GameObject entry = radialsItems[i].gameObject;
            rect.DOAnchorPos(Vector3.zero, 0.3f).SetEase(Ease.OutQuad).onComplete =
                delegate ()
                {
                    Destroy(entry);
                };
        }
        radialsItems.Clear();
        Node.selectedNode.GetComponent<Renderer>().material.color = GameManager.gameManager.nodeColor;
        Node.selectedNode = null;
        radialMenuHasOpen = false;
    }

    private void PlaceUI()
    {
        float radiansOfSeparation = (Mathf.PI * 2) / radialsItems.Count;
        for (int i = 0; i < radialsItems.Count; i++)
        {
            float x = Mathf.Sin(radiansOfSeparation * i) * GameManager.gameManager.radialMenuRadius;
            float y = Mathf.Cos(radiansOfSeparation * i) * GameManager.gameManager.radialMenuRadius;
            RectTransform rect = radialsItems[i].GetComponent<RectTransform>();
            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad).SetDelay(0.05f * i);
            rect.DOAnchorPos(new Vector3(x, y, 0), 0.3f).SetEase(Ease.OutQuad).SetDelay(0.05f * i);
        }
    }

    private GameObject FindUpgradePrefab()
    {
        GameObject upgradePrefab = null;
        for (int i = 0; i < GameManager.gameManager.towersList.Length; i++)
        {
            for (int j = 0; j < GameManager.gameManager.towersList[i].towersPrefabs.Length; j++)
            {
                if (GameManager.gameManager.towersList[i].towersPrefabs[j].tag == Node.selectedNode.GetTower().tag)
                {
                    upgradePrefab = GameManager.gameManager.towersList[i].towersPrefabs[j + 1];
                }
            }
        }
        return upgradePrefab;
    }

    private string FindUpgradePrice()
    {
        int upgradePrice = 0;
        for (int i = 0; i < GameManager.gameManager.towersList.Length; i++)
        {
            for (int j = 0; j < GameManager.gameManager.towersList[i].towersPrefabs.Length; j++)
            {
                if (GameManager.gameManager.towersList[i].towersPrefabs[j].tag == Node.selectedNode.GetTower().tag)
                {
                    upgradePrice = GameManager.gameManager.towersList[i].prices[j + 1];
                }
            }
        }
        return upgradePrice.ToString();
    }
}

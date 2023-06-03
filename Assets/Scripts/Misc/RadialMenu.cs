using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RadialMenu : MonoBehaviour
{
    public static RadialMenu radialMenu;

    [SerializeField]
    private GameObject radialItem;

    [SerializeField]
    private Texture deleteIcon;

    [SerializeField]
    private Texture cancelIcon;

    private List<RadialMenuEntry> radialsItems = new List<RadialMenuEntry>();

    private bool radialMenuHasOpen = false;

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
        for (int i = 0; i < GameManager.gameManager.GetTowers().Length; i++)
        {
            AddTowerButton(GameManager.gameManager.GetTowers()[i].GetTowersPrefabs()[0], GameManager.gameManager.GetTowers()[i].GetIcons()[0], GameManager.gameManager.GetTowers()[i].GetPrices()[0].ToString());
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
        towerButtonScript.SetPrefab(prefab);
        towerButtonScript.SetIcon(texture);
        towerButtonScript.SetLabel(text);
        radialsItems.Add(towerButtonScript);
    }

    private void AddDeleteButton()
    {
        GameObject deleteButton = Instantiate(radialItem, transform);
        RadialMenuEntry deleteButtonScript = deleteButton.GetComponent<RadialMenuEntry>();
        deleteButtonScript.SetIcon(deleteIcon);
        deleteButtonScript.SetLabel("Delete");
        deleteButtonScript.SetDeleteButton(true);
        radialsItems.Add(deleteButtonScript);
    }

    private void AddUpgradeButton()
    {
        GameObject updateButton = Instantiate(radialItem, transform);
        RadialMenuEntry updateButtonScript = updateButton.GetComponent<RadialMenuEntry>();
        updateButtonScript.SetPrefab(FindUpgradePrefab());
        updateButtonScript.SetIcon(FindUpgradeIcon());
        updateButtonScript.SetLabel(FindUpgradePrice());
        updateButtonScript.SetUpgradeButton(true);
        radialsItems.Add(updateButtonScript);
    }

    private void AddCancelButton()
    {
        GameObject cancelButton = Instantiate(radialItem, transform);
        RadialMenuEntry cancelButtonScript = cancelButton.GetComponent<RadialMenuEntry>();
        cancelButtonScript.SetIcon(cancelIcon);
        cancelButtonScript.SetLabel("Cancel");
        cancelButtonScript.SetCancelButton(true);
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
        Node.selectedNode.GetComponent<Renderer>().material.color = GameManager.gameManager.GetNodeColor();
        Node.selectedNode = null;
        radialMenuHasOpen = false;
    }

    private void PlaceUI()
    {
        float radiansOfSeparation = (Mathf.PI * 2) / radialsItems.Count;
        for (int i = 0; i < radialsItems.Count; i++)
        {
            float x = Mathf.Sin(radiansOfSeparation * i) * GameManager.gameManager.GetRadialMenuRadius();
            float y = Mathf.Cos(radiansOfSeparation * i) * GameManager.gameManager.GetRadialMenuRadius();
            RectTransform rect = radialsItems[i].GetComponent<RectTransform>();
            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad).SetDelay(0.05f * i);
            rect.DOAnchorPos(new Vector3(x, y, 0), 0.3f).SetEase(Ease.OutQuad).SetDelay(0.05f * i);
        }
    }

    private GameObject FindUpgradePrefab()
    {
        GameObject upgradePrefab = null;
        for (int i = 0; i < GameManager.gameManager.GetTowers().Length; i++)
        {
            for (int j = 0; j < GameManager.gameManager.GetTowers()[i].GetTowersPrefabs().Length; j++)
            {
                if (GameManager.gameManager.GetTowers()[i].GetTowersPrefabs()[j].tag == Node.selectedNode.GetTower().tag)
                {
                    upgradePrefab = GameManager.gameManager.GetTowers()[i].GetTowersPrefabs()[j + 1];
                }
            }
        }
        return upgradePrefab;
    }

    private string FindUpgradePrice()
    {
        int upgradePrice = 0;
        for (int i = 0; i < GameManager.gameManager.GetTowers().Length; i++)
        {
            for (int j = 0; j < GameManager.gameManager.GetTowers()[i].GetTowersPrefabs().Length; j++)
            {
                if (GameManager.gameManager.GetTowers()[i].GetTowersPrefabs()[j].tag == Node.selectedNode.GetTower().tag)
                {
                    upgradePrice = GameManager.gameManager.GetTowers()[i].GetPrices()[j + 1];
                }
            }
        }
        return upgradePrice.ToString();
    }

    private Texture FindUpgradeIcon()
    {
        Texture upgradeIcon = null;
        for (int i = 0; i < GameManager.gameManager.GetTowers().Length; i++)
        {
            for (int j = 0; j < GameManager.gameManager.GetTowers()[i].GetTowersPrefabs().Length; j++)
            {
                if (GameManager.gameManager.GetTowers()[i].GetTowersPrefabs()[j].tag == Node.selectedNode.GetTower().tag)
                {
                    upgradeIcon = GameManager.gameManager.GetTowers()[i].GetIcons()[j + 1];
                }
            }
        }
        return upgradeIcon;
    }

    public bool GetRadialMenuHasOpen()
    {
        return radialMenuHasOpen;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI label;

    [SerializeField]
    private RawImage icon;

    private GameObject prefab;

    private bool deleteButton = false;

    private bool cancelButton = false;

    private bool upgradeButton = false;

    private void Update()
    {
        if (!deleteButton && !cancelButton && System.Convert.ToInt32(label.text) > GameManager.gameManager.GetPlayers()[0].GetMoney())
        {
            label.color = Color.red;
        }
        else
        {
            label.color = Color.white;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (deleteButton)
        {
            Destroy(Node.selectedNode.GetTower());
            Node.selectedNode.SetTower(null);
            Node.selectedNode.SetLevelTower(0);
            RadialMenu.radialMenu.Close();
        }
        else if (cancelButton)
        {
            RadialMenu.radialMenu.Close();
        }
        else if (upgradeButton && GameManager.gameManager.GetPlayers()[0].GetMoney() >= System.Convert.ToInt32(label.text))
        {
            Destroy(Node.selectedNode.GetTower());
            Node.selectedNode.SetTower(null);
            Node.selectedNode.SetLevelTower(Node.selectedNode.GetLevelTower()+1);
            GameManager.gameManager.GetPlayers()[0].SetMoney(GameManager.gameManager.GetPlayers()[0].GetMoney() - System.Convert.ToInt32(label.text));
            Node.selectedNode.BuildTower(prefab);
            RadialMenu.radialMenu.Close();
        }
        else if (GameManager.gameManager.GetPlayers()[0].GetMoney() >= System.Convert.ToInt32(label.text))
        {
            Node.selectedNode.SetLevelTower(1);
            GameManager.gameManager.GetPlayers()[0].SetMoney(GameManager.gameManager.GetPlayers()[0].GetMoney() - System.Convert.ToInt32(label.text));
            Node.selectedNode.BuildTower(prefab);
            RadialMenu.radialMenu.Close();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOComplete();
        GetComponent<RectTransform>().DOScale(Vector3.one * 1.1f, .3f).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOComplete();
        GetComponent<RectTransform>().DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad);
    }

    public void SetDeleteButton(bool deleteButton)
    {
        this.deleteButton = deleteButton;
    }

    public void SetCancelButton(bool cancelButton)
    {
        this.cancelButton = cancelButton;
    }

    public void SetUpgradeButton(bool upgradeButton)
    {
        this.upgradeButton = upgradeButton;
    }

    public void SetLabel(string text)
    {
        label.text = text;
    }

    public void SetIcon(Texture texture)
    {
        icon.texture = texture;
    }

    public void SetPrefab(GameObject prefab)
    {
        this.prefab = prefab;
    }
}

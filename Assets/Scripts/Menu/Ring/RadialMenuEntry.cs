using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void RingMenuEntryDelegete(RadialMenuEntry entry);
    public TextMeshProUGUI label;
    public RawImage icon;
    public GameObject prefab;
    public bool deleteButton = false;
    public bool cancelButton = false;
    public bool upgradeButton = false;

    private void Update()
    {
        if (!deleteButton && !cancelButton && System.Convert.ToInt32(label.text) > GameManager.gameManager.players[0].money)
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
        else if (upgradeButton && GameManager.gameManager.players[0].money >= System.Convert.ToInt32(label.text))
        {
            Destroy(Node.selectedNode.GetTower());
            Node.selectedNode.SetTower(null);
            Node.selectedNode.SetLevelTower(Node.selectedNode.GetLevelTower()+1);
            GameManager.gameManager.players[0].money -= System.Convert.ToInt32(label.text);
            Node.selectedNode.BuildTower(prefab);
            RadialMenu.radialMenu.Close();
        }
        else if (GameManager.gameManager.players[0].money >= System.Convert.ToInt32(label.text))
        {
            Node.selectedNode.SetLevelTower(1);
            GameManager.gameManager.players[0].money -= System.Convert.ToInt32(label.text);
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
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void RingMenuEntryDelegete(RadialMenuEntry entry);
    public RadialMenu radialmenu;
    public TextMeshProUGUI label;
    public RawImage icon;
    public  GameObject prefab;

    private RectTransform rect;
    
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BuildManager.BuildTurret(prefab, label.text);
        radialmenu.Close();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one * 1.1f, .3f).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad);
    }
}

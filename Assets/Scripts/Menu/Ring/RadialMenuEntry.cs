using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public BuildManager buildManager;

    public delegate void RingMenuEntryDelegete(RadialMenuEntry pEntry);

    public RadialMenu radialmenu;

    RingMenuEntryDelegete callback;
    
    public TextMeshProUGUI label;

    
    public RawImage icon;

    RectTransform rect;

    GameObject prefab;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        buildManager = BuildManager.instance;
    }


    // set le label de l'�lement
    public void Setlabel(string pText)
    {
        label.text = pText;
    }

    // set le l'icon de l'�lement

    public void SetIcon(Texture pIcon)
    {
        icon.texture = pIcon;
    }
    // recup�re le l'icon de l'�lement

    public Texture GetIcon()
    {
        return (icon.texture);
    }

    // recup�re le la prefab de l'�lement

    public void SetPrefab(GameObject pPrefab)
    {
        prefab = pPrefab;
    }
    // action au clique

    public void OnPointerClick(PointerEventData eventData)
    {
        buildManager.SetTurretToBuild(this.prefab);
        buildManager.SetUp = true;
        radialmenu.BuildTurret();
        radialmenu.Close();
    }

    // action de zoom

    public void SetCallback(RingMenuEntryDelegete pCallback)
    {
        callback = pCallback;
    }

    // action a l'entr� de la zonne

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one * 1.1f, .3f).SetEase(Ease.OutQuad);
    }

    // action a la sortie de la zonne


    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad);
    }
}

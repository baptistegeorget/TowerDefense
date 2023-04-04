using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public bool hasTurret = false;

    public RadialMenu radialMenu;

    private Renderer rend;
    private Color hoverColor = Color.yellow;
    private Color startColor;
    
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    
    private void OnMouseDown()
    {
        if (hasTurret == false)
        {
            radialMenu.Toggle(this);
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        rend.material.color = hoverColor;
    }
}
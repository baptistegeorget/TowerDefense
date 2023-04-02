using UnityEngine;

public class Node : MonoBehaviour
{
    public RadialMenu radialMenu;

    private Renderer rend;
    private Color hoverColor = Color.yellow;
    private Color startColor;
    private bool possessesTurret = false;
    
    private void Start()
    {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseDown()
    {
        // ouverture du menu ring
        if (possessesTurret == false)
        {
            radialMenu.Toggle(transform);

        }

    }

    void Update()
    {

    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
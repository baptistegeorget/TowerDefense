using UnityEngine;

public class Node : MonoBehaviour
{
    private int levelTower = 0;
    private Color color;
    private Color hoverColor;
    private Renderer rend;

    private void Start()
    {
        color = GameManager.gameManager.nodeColor;
        hoverColor = GameManager.gameManager.nodeHoverColor;
        rend = GetComponent<Renderer>();
        rend.material.color = color;
    }

    private void OnMouseDown()
    {
        if (!GameManager.gameManager.radialMenuHasOpen)
        {
            GameManager.gameManager.radialMenuHasOpen = true;
            //GameManager.gameManager.radialMenu.Toggle(levelTower);
        }
    }

    private void OnMouseExit()
    {
        if (!GameManager.gameManager.radialMenuHasOpen)
        {
            rend.material.color = color;
        }
    }

    private void OnMouseEnter()
    {
        if (!GameManager.gameManager.radialMenuHasOpen)
        {
            rend.material.color = hoverColor;
        }
    }
}
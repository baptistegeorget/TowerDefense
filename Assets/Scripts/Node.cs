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
        if (!GameManager.gameManager.GetRadialMenuHasOpen())
        {
            GameManager.gameManager.SetRadialMenuHasOpen(true);
            GameManager.gameManager.GetRadialMenu().Toggle(levelTower);
        }
    }

    private void OnMouseExit()
    {
        if (!GameManager.gameManager.GetRadialMenuHasOpen())
        {
            rend.material.color = color;
        }
    }

    private void OnMouseEnter()
    {
        if (!GameManager.gameManager.GetRadialMenuHasOpen())
        {
            rend.material.color = hoverColor;
        }
    }
}
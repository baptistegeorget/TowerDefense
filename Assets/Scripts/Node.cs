using UnityEngine;

public class Node : MonoBehaviour
{
    private int levelTower = 0;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = GameManager.gameManager.nodeColor;
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
            rend.material.color = GameManager.gameManager.nodeColor;
        }
    }

    private void OnMouseEnter()
    {
        if (!GameManager.gameManager.GetRadialMenuHasOpen())
        {
            rend.material.color = GameManager.gameManager.nodeHoverColor;
        }
    }
}
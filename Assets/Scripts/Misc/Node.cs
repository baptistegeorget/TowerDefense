using UnityEngine;

public class Node : MonoBehaviour
{
    public static Node selectedNode;

    private int levelTower = 0;
    private GameObject tower;

    private void Start()
    {
        GetComponent<Renderer>().material.color = GameManager.gameManager.nodeColor;
    }

    private void OnMouseUpAsButton()
    {
        if (!RadialMenu.radialMenuHasOpen)
        {
            selectedNode = this;
            RadialMenu.radialMenu.Toggle();
        }
    }

    private void OnMouseExit()
    {
        if (!RadialMenu.radialMenuHasOpen)
        {
            GetComponent<Renderer>().material.color = GameManager.gameManager.nodeColor;
        }
    }

    private void OnMouseOver()
    {
        if (!RadialMenu.radialMenuHasOpen)
        {
            GetComponent<Renderer>().material.color = GameManager.gameManager.nodeHoverColor;
        }
    }

    public void BuildTower(GameObject towerPrefab)
    {
        tower = Instantiate(towerPrefab, transform.position + new Vector3(0f,1f,0f), new Quaternion());
        tower.transform.SetParent(GameManager.gameManager.transform.GetChild(3));
    }

    public void SetLevelTower(int level)
    {
        levelTower = level;
    }

    public int GetLevelTower()
    {
        return levelTower;
    }

    public void SetTower(GameObject towerPrefab)
    {
        tower = towerPrefab;
    }

    public GameObject GetTower()
    {
        return tower;
    }
}
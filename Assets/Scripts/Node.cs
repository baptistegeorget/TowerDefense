using UnityEngine;

public class Node : MonoBehaviour
{
    private Renderer rend;

    private Color hoverColor = Color.yellow;

    private GameObject turret;

    public Color startColor;

    public Vector3 positionOffset = new Vector3(0, 1.2f, 0);
     
    // Start is called before the first frame update
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
        if (turret != null)
        {
            Debug.Log("Imo");
            return;
        }
        GameObject TurretToBuild = BuildManager.instance.getTurretToBuild();
        turret = (GameObject)Instantiate(TurretToBuild, transform.position + positionOffset, Quaternion.Euler(new Vector3(-90, 0, 0)));
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
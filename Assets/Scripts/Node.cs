using UnityEngine;

public class Node : MonoBehaviour
{
    //informations de la node
    private Renderer rend;
    private Color hoverColor = Color.yellow;
    public Color startColor;

    // informations pour le menu ring
    private bool possessesTurret = false;
    public RadialMenu radialMenu;





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
        // ouverture du menu ring
        if (possessesTurret == false)
        {
            radialMenu.Toggle(this.transform);

        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public Color nodeColor;
    public Color nodeHoverColor;
    public TurretBlueprint[] towerListMenu;

    private RadialMenu radialMenu;
    private bool radialMenuHasOpen = false;

    //private Player player = new Player(20, 300);

    private void Awake()
    {
        radialMenu = new RadialMenu();
        gameManager = this;
    }

    public bool GetRadialMenuHasOpen()
    {
        return radialMenuHasOpen;
    }

    public void SetRadialMenuHasOpen(bool radialMenuHasOpen)
    {
        this.radialMenuHasOpen = radialMenuHasOpen;
    }

    public RadialMenu GetRadialMenu()
    {
        return radialMenu;
    }
}

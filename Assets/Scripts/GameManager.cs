using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public Color nodeColor;
    public Color nodeHoverColor;
    public bool radialMenuHasOpen = false;
    public TurretBlueprint[] towerListMenu;
    //public RadialMenu radialMenu;

    //private Player player = new Player(20, 300);


    private void Start()
    {
        gameManager = this;
        //radialMenu = new RadialMenu(towerListMenu);
    }

    private void Update()
    {
        
    }
}

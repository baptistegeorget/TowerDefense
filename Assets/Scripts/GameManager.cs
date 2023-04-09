using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    // Variables à setup dans Unity
    public PlayerBlueprint[] players;
    public TowerBlueprint[] towersList;
    public Color nodeColor;
    public Color nodeHoverColor;
    public int radialMenuRadius;
    public Vector3 towerPosition;

    private void Awake()
    {
        gameManager = this;
    }
}

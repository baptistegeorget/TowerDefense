using TMPro;
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

    public TextMeshProUGUI life;
    public TextMeshProUGUI money;
    public TextMeshProUGUI waveTimer;
    public TextMeshProUGUI waveCount;

    private void Awake()
    {
        gameManager = this;
    }

    private void Update()
    {
        life.text = players[0].pv.ToString();
        money.text = players[0].money.ToString();
        waveCount.text = WaveSpawner.waveSpawner.waveNumber.ToString() + "/" + WaveSpawner.waveSpawner.waves.Length.ToString();
        waveTimer.text = Mathf.Round(WaveSpawner.waveSpawner.countdown).ToString();
    }
}

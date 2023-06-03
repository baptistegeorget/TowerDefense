using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [Header("Players")]
    [SerializeField]
    private Player[] players;

    [Header("Towers")]
    [SerializeField]
    private Tower[] towers;

    [Header("Waves")]
    [SerializeField]
    private Wave[] waves;
    public float timeBetweenWaves;
    public GameObject Boulepic;
    public GameObject Serpent;
    public GameObject Tank;
    public GameObject Bat;
    public GameObject Healer;
    public GameObject Ghost;
    public GameObject Dragon;
    public GameObject Rainette;
    public GameObject Invocateur;
    public GameObject Ninja;
    public GameObject Centaure;
    public GameObject Lapinou;
    public GameObject Chaman;
    public GameObject Slime;
    public GameObject Victime;

    [Header("Nodes")]
    [SerializeField]
    private Color nodeColor;

    [SerializeField]
    private Color nodeHoverColor;

    [Header("GUI")]
    public TextMeshProUGUI waveTimer;
    public TextMeshProUGUI life;
    public TextMeshProUGUI money;
    public TextMeshProUGUI waveCount;

    private int radialMenuRadius = 300;

    private float countdown;

    private int waveNumber;

    private void Awake()
    {
        gameManager = this;
    }

    private void Update()
    {
        life.text = players[0].GetPv().ToString();
        money.text = players[0].GetMoney().ToString();
        waveCount.text = waveNumber + "/" + waves.Length.ToString();
        waveTimer.text = Mathf.Round(countdown).ToString();
    }

    public void SetCountdown(float countdown)
    {
        this.countdown = countdown;
    }

    public void SetWaveNumber(int waveNumber)
    {
        this.waveNumber = waveNumber;
    }

    public Tower[] GetTowers()
    {
        return towers;
    }

    public Player[] GetPlayers()
    {
        return players;
    }

    public Wave[] GetWaves()
    {
        return waves;
    }

    public Color GetNodeColor()
    {
        return nodeColor;
    }

    public Color GetNodeHoverColor()
    {
        return nodeHoverColor;
    }

    public int GetRadialMenuRadius()
    {
        return radialMenuRadius;
    }
}

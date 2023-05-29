using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [SerializeField]
    private Player[] players;

    [SerializeField]
    private Tower[] towers;

    public Color nodeColor;
    public Color nodeHoverColor;
    public int radialMenuRadius;
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
    public Wave[] waves;
    public float timeBetweenWaves;
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
        life.text = players[0].GetPv().ToString();
        money.text = players[0].GetMoney().ToString();
        waveCount.text = WaveCount().ToString() + "/" + waves.Length.ToString();
        waveTimer.text = Countdown().ToString();
        if (players[0].GetPv() == 0)
        {
            
        }
        if (WaveSpawner.waveSpawner.GetWaveNumber() == waves.Length && WaveSpawner.waveSpawner.GetEnemiesAlives() == 0)
        {
            
        }
    }

    private float Countdown()
    {
        if (WaveSpawner.waveSpawner.GetEnemiesAlives() > 0 || Mathf.Round(WaveSpawner.waveSpawner.GetCountdown()) == timeBetweenWaves)
        {
            return 0f;
        }
        else
        {
            return Mathf.Round(WaveSpawner.waveSpawner.GetCountdown());
        }
    }

    private int WaveCount()
    {
        if (WaveSpawner.waveSpawner.GetWaveNumber() == waves.Length)
        {
            return waves.Length;
        }
        else
        {
            return WaveSpawner.waveSpawner.GetWaveNumber() + 1;
        }
    }

    public Tower[] GetTowers()
    {
        return towers;
    }

    public Player[] GetPlayers()
    {
        return players;
    }
}

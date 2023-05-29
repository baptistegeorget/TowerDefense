using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    // Variables � setup dans Unity
    public GameObject spawnPoint;
    public PlayerBlueprint[] players;
    public TowerBlueprint[] towersList;
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
    public Canvas gameOverCanvas;
    public Canvas winCanvas;
    public int nextLevelNumber = 2;
    public string dificulty = "levelReachedDebutant";
    
    

    private void Awake()
    {
        gameManager = this;
        gameOverCanvas.enabled = false;
        winCanvas.enabled = false;
    }

    private void Update()
    {
        life.text = players[0].pv.ToString();
        money.text = players[0].money.ToString();
        waveCount.text = WaveCount().ToString() + "/" + waves.Length.ToString();
        waveTimer.text = Countdown().ToString();
        if (players[0].pv == 0)
        {
            CameraController.cameraLock = true;
            gameOverCanvas.enabled = true;
            Node.disable = true;
            
            
        }
        if (WaveSpawner.waveSpawner.GetWaveNumber() == waves.Length && WaveSpawner.enemiesAlives == 0)
        {
            PlayerPrefs.SetInt(dificulty, nextLevelNumber);
            CameraController.cameraLock = true;
            Node.disable = true;
            winCanvas.enabled = true;

        }
    }

    private float Countdown()
    {
        if (WaveSpawner.enemiesAlives > 0 || Mathf.Round(WaveSpawner.waveSpawner.GetCountdown()) == timeBetweenWaves)
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
}

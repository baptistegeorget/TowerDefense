using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMulti : MonoBehaviourPunCallbacks
{
    public static GameManagerMulti gameManager;

    [Header("Towers")]
    [SerializeField]
    private Tower[] towers;

    [Header("Waves")]
    [SerializeField]
    private int moneyByHealth;

    [SerializeField]
    private Wave[] waves;

    [SerializeField]
    private float timeBetweenWaves;

    [SerializeField]
    private GameObject boulepic;

    [SerializeField]
    private GameObject serpent;

    [SerializeField]
    private GameObject tank;

    [SerializeField]
    private GameObject bat;

    [SerializeField]
    private GameObject healer;

    [SerializeField]
    private GameObject ghost;

    [SerializeField]
    private GameObject dragon;

    [SerializeField]
    private GameObject rainette;

    [SerializeField]
    private GameObject invocateur;

    [SerializeField]
    private GameObject ninja;

    [SerializeField]
    private GameObject centaure;

    [SerializeField]
    private GameObject lapinou;

    [SerializeField]
    private GameObject chaman;

    [SerializeField]
    private GameObject slime;

    [SerializeField]
    private GameObject victime;

    [Header("Nodes")]
    [SerializeField]
    private Color nodeColor;

    [SerializeField]
    private Color nodeHoverColor;

    [Header("GUI")]
    [SerializeField]
    private TextMeshProUGUI waveTimer;

    [SerializeField]
    private TextMeshProUGUI life;

    [SerializeField]
    private TextMeshProUGUI money;

    [SerializeField]
    private TextMeshProUGUI waveCount;

    [SerializeField]
    private GameObject skipButton;

    [SerializeField]
    private GameObject breakMenu;

    [SerializeField]
    private GameObject winMenu;

    [SerializeField]
    private GameObject looseMenu;

    private List<GameObject> players;

    private int radialMenuRadius = 300;

    private float countdown;

    private int waveNumber;

    private bool endWaves;

    private string[] enemiesTags = { "Boulepic", "Slime", "Centaure", "Chaman", "Chauve-souris", "Dragon", "Ghost", "Healer", "Invocateur", "Lapinou", "Ninja", "Rainette", "Serpent", "Tank", "Victime" };

    private void Awake()
    {
        gameManager = this;
    }

    private void Start()
    {
        FindPlayers();
    }

    //private void Update()
    //{
    //    life.text = players[0].GetPv().ToString();
    //    money.text = players[0].GetMoney().ToString();
    //    waveCount.text = waveNumber + "/" + waves.Length.ToString();
    //    waveTimer.text = Mathf.Round(countdown).ToString();
    //    if (players[0].GetPv() <= 0)
    //    {
    //        looseMenu.SetActive(true);
    //    }
    //    if (endWaves && WaveSpawner.waveSpawner.GetEnemiesAlives() == 0)
    //    {
    //        winMenu.SetActive(true);
    //    }
    //}

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        PhotonView[] photonViews = FindObjectsOfType<PhotonView>();
        foreach (var photonView in photonViews)
        {
            if (otherPlayer.NickName == photonView.Owner.NickName || photonView.Owner.NickName == null)
            {

            }
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        FindPlayers();
    }

    private void FindPlayers()
    {
        PhotonView[] photonViews = FindObjectsOfType<PhotonView>();
        foreach (var photonView in photonViews)
        {
            bool find = false;
            foreach (var player in players)
            {
                if (photonView.ViewID == player.GetPhotonView().ViewID)
                {
                    find = true;
                    break;
                }
            }
            if (!find)
            {
                players.Add(photonView.gameObject);
            }
        }
    }

    //public void SkipWave()
    //{
    //    if (countdown == 0)
    //    {
    //        players[0].SetMoney(players[0].GetMoney() + (int)timeBetweenWaves);
    //        GameObject[] enemies = { };
    //        foreach (string enemyTag in enemiesTags)
    //        {
    //            GameObject[] temp = GameObject.FindGameObjectsWithTag(enemyTag);
    //            enemies = enemies.Concat(temp).ToArray();
    //        }
    //        foreach (var enemy in enemies)
    //        {
    //            players[0].SetMoney(players[0].GetMoney() + 1 + enemy.GetComponent<Enemy>().GetMoney() / 2);
    //        }
    //    }
    //    else
    //    {
    //        players[0].SetMoney(players[0].GetMoney() + (int)countdown);
    //    }
    //    WaveSpawner.waveSpawner.StartNextWave();
    //    skipButton.SetActive(false);
    //}

    public void Break()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0;
            breakMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            breakMenu.SetActive(false);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void QuitLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void DisplaySkipButton()
    {
        skipButton.SetActive(true);
    }

    public void HiddenSkipButton()
    {
        skipButton.SetActive(false);
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

    public List<GameObject> GetPlayers()
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

    public float GetTimeBetweenWaves()
    {
        return timeBetweenWaves;
    }

    public GameObject GetBoulepic()
    {
        return boulepic;
    }

    public GameObject GetSerpent()
    {
        return serpent;
    }

    public GameObject GetTank()
    {
        return tank;
    }

    public GameObject GetBat()
    {
        return bat;
    }

    public GameObject GetHealer()
    {
        return healer;
    }

    public GameObject GetGhost()
    {
        return ghost;
    }

    public GameObject GetDragon()
    {
        return dragon;
    }

    public GameObject GetRainette()
    {
        return rainette;
    }

    public GameObject GetInvocateur()
    {
        return invocateur;
    }

    public GameObject GetNinja()
    {
        return ninja;
    }

    public GameObject GetCentaure()
    {
        return centaure;
    }

    public GameObject GetLapinou()
    {
        return lapinou;
    }

    public GameObject GetChaman()
    {
        return chaman;
    }

    public GameObject GetSlime()
    {
        return slime;
    }

    public GameObject GetVictime()
    {
        return victime;
    }

    public int GetMoneyByHealth()
    {
        return moneyByHealth;
    }

    public void SetEndWaves(bool endWaves)
    {
        this.endWaves = endWaves;
    }
}

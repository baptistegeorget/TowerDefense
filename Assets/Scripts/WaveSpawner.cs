using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform slimePrefab;
    public Transform boulepicPrefab;
    public Transform centaurePrefab;
    public Transform chamanPrefab;
    public Transform chauvesourisPrefab;
    public Transform dragonPrefab;
    public Transform ghostPrefab;
    public Transform healerPrefab;
    public Transform invocateurPrefab;
    public Transform lapinouPrefab;
    public Transform ninjaPrefab;
    public Transform rainettePrefab;
    public Transform serpentPrefab;
    public Transform tankPrefab;
    public Transform victimePrefab;
    public Transform enemyPrefab;

    public static int EnemiesAlives = 0;

    public Transform spawnPoint;
    public float timeBetweenWaves = 5.5f;
    public Text waveCountdownTimer;
    private float countdown = 2f;
    private int waveNumber = 0;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        waveCountdownTimer.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlives++;
    }
}

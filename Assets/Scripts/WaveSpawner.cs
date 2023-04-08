using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    public GameObject enemy7;
    public GameObject enemy8;
    public GameObject enemy9;
    public GameObject enemy10;
    public GameObject enemy11;
    public GameObject enemy12;
    public GameObject enemy13;
    public GameObject enemy14;
    public GameObject enemy15;
    public GameObject[] arrayEnemy;

    public static int EnemiesAlives = 0;

    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5.5f;

    private float countdown = 2f;
    private int waveNumber = 0;


    void AllEnemies(Wave wave)
    {
        arrayEnemy = new GameObject[wave.enemy1Count + wave.enemy2Count + wave.enemy3Count + wave.enemy4Count + wave.enemy5Count + wave.enemy6Count + wave.enemy7Count + wave.enemy8Count
             + wave.enemy9Count + wave.enemy10Count + wave.enemy11Count + wave.enemy12Count + wave.enemy13Count + wave.enemy14Count + wave.enemy15Count];
        for (int i = 0; i < wave.enemy1Count; i++)
        {
            arrayEnemy[i] = enemy1;
        }
        for (int j = wave.enemy1Count; j < wave.enemy2Count + wave.enemy1Count; j++)
        {
            arrayEnemy[j] = enemy2;
        }
        for (int k = wave.enemy2Count + wave.enemy1Count; k < wave.enemy3Count + wave.enemy2Count + wave.enemy1Count; k++)
        {
            arrayEnemy[k] = enemy3;
        }
        for (int l = wave.enemy3Count + wave.enemy2Count + wave.enemy1Count; l < wave.enemy4Count + wave.enemy3Count + wave.enemy2Count + wave.enemy1Count; l++)
        {
            arrayEnemy[l] = enemy4;
        }
    }



    void SuffleArrayEnemy()
    {
        for (int i = 0; i < arrayEnemy.Length; i++)
        {
            int random = Random.Range(i, arrayEnemy.Length);
            GameObject tempGO = arrayEnemy[random];
            arrayEnemy[random] = arrayEnemy[i];
            arrayEnemy[i] = tempGO;
        } 
    }


    void Update(){
        if (EnemiesAlives > 0)
        {
            return;
        }
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveNumber];
        AllEnemies(wave);
        SuffleArrayEnemy();

        for (int i = 0; i < (arrayEnemy.Length); i++)
        {
            StartCoroutine(SpawnEnemy(arrayEnemy[i]));
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNumber++;

        if (waveNumber == waves.Length)
        {
            Debug.Log("BRAVO");
            this.enabled = false;
        }
    }

    IEnumerator SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(1f/2);
        EnemiesAlives++;
    }
}

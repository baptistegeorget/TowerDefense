using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner waveSpawner;
    public static int enemiesAlives = 0;
    
    private int waveNumber = 0;
    private List<GameObject> listEnemy;
    private float countdown;

    private void Start()
    {
        countdown = GameManager.gameManager.timeBetweenWaves;
        waveSpawner = this;
    }

    private void Update()
    {
        if (enemiesAlives > 0)
        {
            return;
        }
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = GameManager.gameManager.timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
    }

    private void AddEnemies(Wave wave)
    {
        listEnemy = new List<GameObject>();
        AddEnemy(wave.enemy1Count, GameManager.gameManager.enemy1);
        AddEnemy(wave.enemy2Count, GameManager.gameManager.enemy2);
        AddEnemy(wave.enemy3Count, GameManager.gameManager.enemy3);
        AddEnemy(wave.enemy4Count, GameManager.gameManager.enemy4);
        AddEnemy(wave.enemy5Count, GameManager.gameManager.enemy5);
        AddEnemy(wave.enemy6Count, GameManager.gameManager.enemy6);
        AddEnemy(wave.enemy7Count, GameManager.gameManager.enemy7);
        AddEnemy(wave.enemy8Count, GameManager.gameManager.enemy8);
        AddEnemy(wave.enemy9Count, GameManager.gameManager.enemy9);
        AddEnemy(wave.enemy10Count, GameManager.gameManager.enemy10);
        AddEnemy(wave.enemy11Count, GameManager.gameManager.enemy11);
        AddEnemy(wave.enemy12Count, GameManager.gameManager.enemy12);
        AddEnemy(wave.enemy13Count, GameManager.gameManager.enemy13);
        AddEnemy(wave.enemy14Count, GameManager.gameManager.enemy14);
        AddEnemy(wave.enemy15Count, GameManager.gameManager.enemy15);
    }

    private void AddEnemy(int count, GameObject enemy)
    {
        for (int i = 0; i < count; i++)
        {
            listEnemy.Add(enemy);
        }
    }

    private void SuffleListEnemy()
    {
        for (int i = 0; i < listEnemy.Count; i++)
        {
            int random = Random.Range(i, listEnemy.Count);
            GameObject tempGO = listEnemy[random];
            listEnemy[random] = listEnemy[i];
            listEnemy[i] = tempGO;
        }
    }

    private IEnumerator SpawnWave()
    {
        Wave wave = GameManager.gameManager.waves[waveNumber];
        AddEnemies(wave);
        SuffleListEnemy();
        for (int i = 0; i < (listEnemy.Count); i++)
        {
            StartCoroutine(SpawnEnemy(listEnemy[i]));
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNumber++;
        if (waveNumber == GameManager.gameManager.waves.Length)
        {
            enabled = false;
        }
    }

    private IEnumerator SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, GameManager.gameManager.spawnPoint.transform.position, GameManager.gameManager.spawnPoint.transform.rotation);
        yield return new WaitForSeconds(1f/2);
        enemiesAlives++;
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetCountdown()
    {
        return countdown;
    }
}

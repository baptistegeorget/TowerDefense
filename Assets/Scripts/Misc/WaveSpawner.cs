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
        if (GameManager.gameManager.players[0].pv == 0) {
            enabled = false;
        }
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
        AddEnemy(wave.BoulepicCount, GameManager.gameManager.Boulepic);
        AddEnemy(wave.SerpentCount, GameManager.gameManager.Serpent);
        AddEnemy(wave.TankCount, GameManager.gameManager.Tank);
        AddEnemy(wave.BatCount, GameManager.gameManager.Bat);
        AddEnemy(wave.HealerCount, GameManager.gameManager.Healer);
        AddEnemy(wave.GhostCount, GameManager.gameManager.Ghost);
        AddEnemy(wave.DragonCount, GameManager.gameManager.Dragon);
        AddEnemy(wave.RainetteCount, GameManager.gameManager.Rainette);
        AddEnemy(wave.InvocateurCount, GameManager.gameManager.Invocateur);
        AddEnemy(wave.NinjaCount, GameManager.gameManager.Ninja);
        AddEnemy(wave.CentaureCount, GameManager.gameManager.Centaure);
        AddEnemy(wave.LapinouCount, GameManager.gameManager.Lapinou);
        AddEnemy(wave.ChamanCount, GameManager.gameManager.Chaman);
        AddEnemy(wave.SlimeCount, GameManager.gameManager.Slime);
        AddEnemy(wave.VictimeCount, GameManager.gameManager.Victime);
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner waveSpawner;

    private int enemiesAlives = 0;
    
    private int waveNumber = 0;

    private List<GameObject> listEnemy;

    private float countdown;

    private void Start()
    {
        countdown = GameManager.gameManager.timeBetweenWaves;
        GameManager.gameManager.SetCountdown(countdown);
        GameManager.gameManager.SetWaveNumber(waveNumber + 1);
        waveSpawner = this;
    }

    private void Update()
    {
        if (GameManager.gameManager.GetPlayers()[0].GetPv() == 0) {
            enabled = false;
        }
        if (enemiesAlives > 0)
        {
            return;
        }
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            enemiesAlives += listEnemy.Count;
            GameManager.gameManager.SetWaveNumber(waveNumber + 1);
            countdown = GameManager.gameManager.timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
        GameManager.gameManager.SetCountdown(countdown);
    }

    private void AddEnemies(Wave wave)
    {
        listEnemy = new List<GameObject>();
        AddEnemy(wave.GetBoulepicCount(), GameManager.gameManager.Boulepic);
        AddEnemy(wave.GetSerpentCount(), GameManager.gameManager.Serpent);
        AddEnemy(wave.GetTankCount(), GameManager.gameManager.Tank);
        AddEnemy(wave.GetBatCount(), GameManager.gameManager.Bat);
        AddEnemy(wave.GetHealerCount(), GameManager.gameManager.Healer);
        AddEnemy(wave.GetGhostCount(), GameManager.gameManager.Ghost);
        AddEnemy(wave.GetDragonCount(), GameManager.gameManager.Dragon);
        AddEnemy(wave.GetRainetteCount(), GameManager.gameManager.Rainette);
        AddEnemy(wave.GetInvocateurCount(), GameManager.gameManager.Invocateur);
        AddEnemy(wave.GetNinjaCount(), GameManager.gameManager.Ninja);
        AddEnemy(wave.GetCentaureCount(), GameManager.gameManager.Centaure);
        AddEnemy(wave.GetLapinouCount(), GameManager.gameManager.Lapinou);
        AddEnemy(wave.GetChamanCount(), GameManager.gameManager.Chaman);
        AddEnemy(wave.GetSlimeCount(), GameManager.gameManager.Slime);
        AddEnemy(wave.GetVictimeCount(), GameManager.gameManager.Victime);
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
        Wave wave = GameManager.gameManager.GetWaves()[waveNumber];
        AddEnemies(wave);
        SuffleListEnemy();
        for (int i = 0; i < (listEnemy.Count); i++)
        {
            StartCoroutine(SpawnEnemy(listEnemy[i]));
            yield return new WaitForSeconds(1f / wave.GetRate());
        }
        waveNumber++;
        if (waveNumber == GameManager.gameManager.GetWaves().Length)
        {
            enabled = false;
        }
    }

    private IEnumerator SpawnEnemy(GameObject enemy)
    {
        GameObject enemyTemp = Instantiate(enemy, SpawnPoint.spawnPoint.transform.position, SpawnPoint.spawnPoint.transform.rotation);
        enemyTemp.transform.SetParent(transform);
        yield return new WaitForSeconds(1f/2);
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetCountdown()
    {
        return countdown;
    }

    public int GetEnemiesAlives()
    {
        return enemiesAlives;
    }

    public void SetEnemiesAlives(int enemiesAlives)
    {
        this.enemiesAlives = enemiesAlives;
    }
}

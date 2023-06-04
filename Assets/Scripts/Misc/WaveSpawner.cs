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
        countdown = GameManager.gameManager.GetTimeBetweenWaves();
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
            StartNextWave();
        }
        countdown -= Time.deltaTime;
        GameManager.gameManager.SetCountdown(countdown);
    }

    private void AddEnemies(Wave wave)
    {
        listEnemy = new List<GameObject>();
        AddEnemy(wave.GetBoulepicCount(), GameManager.gameManager.GetBoulepic());
        AddEnemy(wave.GetSerpentCount(), GameManager.gameManager.GetSerpent());
        AddEnemy(wave.GetTankCount(), GameManager.gameManager.GetTank());
        AddEnemy(wave.GetBatCount(), GameManager.gameManager.GetBat());
        AddEnemy(wave.GetHealerCount(), GameManager.gameManager.GetHealer());
        AddEnemy(wave.GetGhostCount(), GameManager.gameManager.GetGhost());
        AddEnemy(wave.GetDragonCount(), GameManager.gameManager.GetDragon());
        AddEnemy(wave.GetRainetteCount(), GameManager.gameManager.GetRainette());
        AddEnemy(wave.GetInvocateurCount(), GameManager.gameManager.GetInvocateur());
        AddEnemy(wave.GetNinjaCount(), GameManager.gameManager.GetNinja());
        AddEnemy(wave.GetCentaureCount(), GameManager.gameManager.GetCentaure());
        AddEnemy(wave.GetLapinouCount(), GameManager.gameManager.GetLapinou());
        AddEnemy(wave.GetChamanCount(), GameManager.gameManager.GetChaman());
        AddEnemy(wave.GetSlimeCount(), GameManager.gameManager.GetSlime());
        AddEnemy(wave.GetVictimeCount(), GameManager.gameManager.GetVictime());
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

    public void StartNextWave()
    {
        GameManager.gameManager.SetCountdown(0);
        StartCoroutine(SpawnWave());
        enemiesAlives += listEnemy.Count;
        GameManager.gameManager.SetWaveNumber(waveNumber + 1);
        countdown = GameManager.gameManager.GetTimeBetweenWaves();
        return;
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
        else
        {
            GameManager.gameManager.DisplaySkipButton();
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

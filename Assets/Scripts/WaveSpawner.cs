using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Ajout de cette ligne pour utiliser les listes

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
    public List<GameObject> listEnemy; // Déclaration de la liste de GameObject

    public static int EnemiesAlives = 0;

    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5.5f;

    private float countdown = 2f;
    private int waveNumber = 0;

    private void Loop(int count, GameObject enemy)
    {
        for (int i = 0; i < count; i++)
        {
            listEnemy.Add(enemy);
        }
    }

    private void AllEnemies(Wave wave)
    {
        listEnemy = new List<GameObject>(); // Initialisation de la liste
        Loop(wave.enemy1Count, enemy1);
        Loop(wave.enemy2Count, enemy2);
        Loop(wave.enemy3Count, enemy3);
        Loop(wave.enemy4Count, enemy4);
        Loop(wave.enemy5Count, enemy5);
        Loop(wave.enemy6Count, enemy6);
        Loop(wave.enemy7Count, enemy7);
        Loop(wave.enemy8Count, enemy8);
        Loop(wave.enemy9Count, enemy9);
        Loop(wave.enemy10Count, enemy10);
        Loop(wave.enemy11Count, enemy11);
        Loop(wave.enemy12Count, enemy12);
        Loop(wave.enemy13Count, enemy13);
        Loop(wave.enemy14Count, enemy14);
        Loop(wave.enemy15Count, enemy15);
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
        Wave wave = waves[waveNumber];
        AllEnemies(wave);
        SuffleListEnemy();

        for (int i = 0; i < (listEnemy.Count); i++)
        {
            StartCoroutine(SpawnEnemy(listEnemy[i]));
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNumber++;

        if (waveNumber == waves.Length)
        {
            Debug.Log("BRAVO");
            enabled = false;
        }
    }

    private IEnumerator SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(1f/2);
        EnemiesAlives++;
    }
    private void Update(){
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
}

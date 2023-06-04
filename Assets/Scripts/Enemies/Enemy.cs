using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float health = 10f;

    [SerializeField]
    private float resistance = 0;

    [SerializeField]
    private Image healthbar;

    [Header("Chaman")]
    [SerializeField]
    private int chamanResistance = 20;

    [Header("Lapinou")]
    [SerializeField]
    private float lapinouSpeed = 1.20f;

    [Header("Healer")]
    [SerializeField]
    public float healerPowerInterval = 1;

    [SerializeField]
    public float healerHeal = 2;

    [Header("Invocateur")]
    [SerializeField]
    private float invocateurPowerInterval = 12;

    [SerializeField]
    private float delayInvocation = 2;

    [SerializeField]
    private GameObject enemy1;

    [SerializeField]
    private GameObject enemy2;

    [SerializeField]
    private GameObject enemy3;

    private int money;

    private float startHealth;

    private float startSpeed;

    private float startResistance;

    private float freezeTime = 0;

    private bool freeze = false;

    private string[] enemiesTags = { "Boulepic", "Slime", "Centaure", "Chaman", "Chauve-souris", "Dragon", "Ghost", "Healer", "Invocateur", "Lapinou", "Ninja", "Rainette", "Serpent", "Tank", "Victime" };

    private bool powerApplied = false;

    private GameObject[] enemyPrefabs;

    private float healerPowerTimer = 0f;

    private float invocateurPowerTimer = 6f;

    private List<GameObject> listEnemyBoost = new List<GameObject>();

    private void Start()
    {
        startSpeed = speed;
        startHealth = health;
        startSpeed = speed;
        startResistance = resistance;
        enemyPrefabs = new GameObject[] { enemy1, enemy2, enemy3 };
        money = (int)Mathf.Round(health * GameManager.gameManager.GetMoneyByHealth() / 100);
        InvokeRepeating("Power", 0, 1f);
    }

    private void Update()
    {
        if (freezeTime > 0)
        {
            speed = 0;
            freezeTime -= Time.deltaTime;
        }
        else if (freeze)
        {
            speed = startSpeed;
            freeze = false;
        }
        if (health <= 0f)
        {
            Die();
        }
        invocateurPowerTimer += Time.deltaTime;
        healerPowerTimer += Time.deltaTime;
    }

    private void Die()
    {
        GameManager.gameManager.GetPlayers()[0].SetMoney(GameManager.gameManager.GetPlayers()[0].GetMoney() + money);
        Destroy(gameObject);
        WaveSpawner.waveSpawner.SetEnemiesAlives(WaveSpawner.waveSpawner.GetEnemiesAlives() - 1);
    }

    public void Damage(float damage)
    {
        health -= damage - (damage * resistance / 100);
        healthbar.fillAmount = health / startHealth;
    }

    public void Freeze(float time)
    {
        freezeTime = time;
        freeze = true;
    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        int enemyIndex = 0;
        while (enemyIndex < enemyPrefabs.Length)
        {
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], transform.position, transform.rotation);
            enemy.transform.SetParent(WaveSpawner.waveSpawner.gameObject.transform);
            enemy.GetComponent<EnemyMovement>().SetTarget(GetComponent<EnemyMovement>().GetTarget());
            enemy.GetComponent<EnemyMovement>().SetWaypoint(GetComponent<EnemyMovement>().GetWaypoint());
            WaveSpawner.waveSpawner.SetEnemiesAlives(WaveSpawner.waveSpawner.GetEnemiesAlives() + 1);
            enemyIndex++;
            yield return new WaitForSeconds(delayInvocation);
        }
    }

    private void Power()
    {
        GameObject[] enemies = { };
        switch (gameObject.GetComponent<Enemy>().tag)
        {
            case "Victime":
                if (!powerApplied && health <= startHealth / 2)
                {
                    speed *= 2f;
                    startSpeed *= 2f;
                    powerApplied = true;
                }
                break;

            case "Chaman":
                foreach (var enemy in listEnemyBoost.ToList())
                {
                    if (enemy != null)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distanceToEnemy >= 2.5)
                        {
                            enemy.GetComponent<Enemy>().resistance = enemy.GetComponent<Enemy>().startResistance;
                            listEnemyBoost.Remove(enemy);
                        }
                    }
                    else
                    {
                        listEnemyBoost.Remove(enemy);
                    }
                }
                foreach (string enemyTag in enemiesTags)
                {
                    GameObject[] temp = GameObject.FindGameObjectsWithTag(enemyTag);
                    enemies = enemies.Concat(temp).ToArray();
                }
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < 2.5 && enemy.GetComponent<Enemy>().tag != "Chaman" && !listEnemyBoost.Contains(enemy))
                    {
                        enemy.GetComponent<Enemy>().resistance = chamanResistance;
                        listEnemyBoost.Add(enemy);
                    }
                }
                break;

            case "Lapinou":
                foreach (var enemy in listEnemyBoost.ToList())
                {
                    if (enemy != null)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distanceToEnemy >= 2.5)
                        {
                            enemy.GetComponent<Enemy>().speed = enemy.GetComponent<Enemy>().startSpeed;
                            listEnemyBoost.Remove(enemy);
                        }
                    }
                    else
                    {
                        listEnemyBoost.Remove(enemy);
                    }
                }
                foreach (string enemyTag in enemiesTags)
                {
                    GameObject[] temp = GameObject.FindGameObjectsWithTag(enemyTag);
                    enemies = enemies.Concat(temp).ToArray();
                }
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < 2.5 && enemy.GetComponent<Enemy>().tag != "Lapinou" && !listEnemyBoost.Contains(enemy))
                    {
                        enemy.GetComponent<Enemy>().speed *= lapinouSpeed;
                        listEnemyBoost.Add(enemy);
                    }
                }
                break;

            case "Invocateur":
                if (invocateurPowerTimer >= invocateurPowerInterval)
                {
                    StartCoroutine(SpawnEnemiesWithDelay());
                    invocateurPowerTimer = 0;
                }
                break;

            case "Healer":
                foreach (string enemyTag in enemiesTags)
                {
                    GameObject[] temp = GameObject.FindGameObjectsWithTag(enemyTag);
                    enemies = enemies.Concat(temp).ToArray();
                }
                if (healerPowerTimer >= healerPowerInterval)
                {
                    foreach (GameObject enemy in enemies)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distanceToEnemy < 2.5 && enemy.GetComponent<Enemy>().tag != "Healer")
                        {
                            if (enemy.GetComponent<Enemy>().health < enemy.GetComponent<Enemy>().startHealth - healerHeal)
                            {
                                enemy.GetComponent<Enemy>().health += healerHeal;
                                enemy.GetComponent<Enemy>().Damage(0);
                            }
                            else
                            {
                                enemy.GetComponent<Enemy>().health = enemy.GetComponent<Enemy>().startHealth;
                                enemy.GetComponent<Enemy>().Damage(0);
                            }
                        }
                    }
                    healerPowerTimer = 0f;
                }
                break;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetMoney()
    {
        return money;
    }
}
